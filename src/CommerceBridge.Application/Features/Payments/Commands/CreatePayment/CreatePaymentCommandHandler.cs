using CommerceBridge.Application.Common.Interfaces;
using CommerceBridge.Application.Common.Models;
using CommerceBridge.Domain.Entities;
using CommerceBridge.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CommerceBridge.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentCommandHandler
    : IRequestHandler<CreatePaymentCommand, CreatePaymentResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPaymentGateway _paymentGateway;

    public CreatePaymentCommandHandler(
        IApplicationDbContext context,
        IPaymentGateway paymentGateway)
    {
        _context = context;
        _paymentGateway = paymentGateway;
    }

    public async Task<CreatePaymentResponse> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var order = await _context.Orders
            .Include(x => x.Payment)
            .FirstOrDefaultAsync(
                x => x.Id == request.OrderId,
                cancellationToken);

        if (order is null)
            throw new InvalidOperationException(
                "Order could not be found.");

        if (order.Status == OrderStatus.Cancelled)
            throw new InvalidOperationException(
                "Cancelled orders cannot be paid.");

        if (order.Status == OrderStatus.Paid)
            throw new InvalidOperationException(
                "Order is already paid.");

        if (order.Payment is not null)
            throw new InvalidOperationException(
                "A payment already exists for this order.");

        var payment = new Payment
        {
            OrderId = order.Id,
            Amount = order.TotalAmount,
            Provider = "Sipay",
            Status = PaymentStatus.Processing
        };

        await _context.Payments.AddAsync(
            payment,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);

        var gatewayResult =
            await _paymentGateway.CreatePaymentAsync(
                new PaymentGatewayRequest(
                    order.Id,
                    order.OrderNumber,
                    order.TotalAmount),
                cancellationToken);

        if (gatewayResult.IsSuccessful)
        {
            payment.Status = PaymentStatus.Paid;

            payment.ProviderTransactionId =
                gatewayResult.TransactionId;

            order.Status = OrderStatus.Paid;

            order.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            payment.Status = PaymentStatus.Failed;

            payment.FailureReason =
                gatewayResult.ErrorMessage;
        }

        payment.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(
            cancellationToken);

        return new CreatePaymentResponse(
            payment.Id,
            payment.OrderId,
            payment.Amount,
            payment.Status.ToString(),
            payment.Provider,
            payment.ProviderTransactionId,
            payment.FailureReason,
            payment.CreatedAt);
    }
}