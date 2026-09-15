using System.Globalization;
using System.Text.Json;
using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Application.Common.Models;
using CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;
using Microsoft.Extensions.Options;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay;

public sealed class SipayPaymentGateway
    : IPaymentGateway
{
    private readonly SipayClient _client;
    private readonly SipayOptions _options;

    public SipayPaymentGateway(
        SipayClient client,
        IOptions<SipayOptions> options)
    {
        _client = client;
        _options = options.Value;
    }

    public async Task<PaymentGatewayResult> CreatePaymentAsync(
        PaymentGatewayRequest request,
        CancellationToken cancellationToken)
    {
        var invoice = new SipayInvoice
        {
            InvoiceId = request.OrderNumber,
            InvoiceDescription =
                $"CommerceBridge order {request.OrderNumber}",

            Total = request.Amount.ToString(
                "0.00",
                CultureInfo.InvariantCulture),

            Discount = 0,
            ReturnUrl = _options.ReturnUrl,
            CancelUrl = _options.CancelUrl,
            BillEmail = request.Email,
            ResponseMethod = "POST",

            Items =
            [
                new SipayInvoiceItem
                {
                    Name = $"Order {request.OrderNumber}",
                    Price = request.Amount.ToString(
                        "0.00",
                        CultureInfo.InvariantCulture),
                    Quantity = 1,
                    Description = "CommerceBridge order"
                }
            ]
        };

        var sipayRequest =
            new SipayPurchaseLinkRequest
            {
                MerchantKey = _options.MerchantKey,
                Name = request.CustomerName,
                Surname = request.CustomerSurname,
                CurrencyCode = "TRY",
                Invoice = JsonSerializer.Serialize(invoice)
            };

        var response =
            await _client.CreatePurchaseLinkAsync(
                sipayRequest,
                cancellationToken);

        if (!response.Status ||
            string.IsNullOrWhiteSpace(response.Link))
        {
            return new PaymentGatewayResult(
                false,
                null,
                null,
                response.SuccessMessage
                    ?? "Sipay payment link could not be created.");
        }

        return new PaymentGatewayResult(
            true,
            response.OrderId,
            response.Link,
            null);
    }
}