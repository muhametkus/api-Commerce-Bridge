using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Application.Common.Models;
using CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay;

public sealed class SipayPaymentGateway
    : IPaymentGateway
{
    private readonly SipayClient _client;

    public SipayPaymentGateway(
        SipayClient client)
    {
        _client = client;
    }

    public async Task<PaymentGatewayResult> CreatePaymentAsync(
        PaymentGatewayRequest request,
        CancellationToken cancellationToken)
    {
        var sipayRequest = new SipayPaymentRequest
        {
            OrderId = request.OrderNumber,
            Amount = request.Amount
        };

        var response = await _client.CreatePaymentAsync(
            sipayRequest,
            cancellationToken);

        return new PaymentGatewayResult(
            response.Success,
            response.TransactionId,
            response.Message);
    }
}