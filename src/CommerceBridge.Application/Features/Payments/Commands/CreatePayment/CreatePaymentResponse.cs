namespace CommerceBridge.Application.Features.Payments.Commands.CreatePayment;

public sealed record CreatePaymentResponse(
    Guid PaymentId,
    Guid OrderId,
    decimal Amount,
    string Status,
    string Provider,
    string? ProviderTransactionId,
    string? PaymentUrl,
    string? FailureReason,
    DateTime CreatedAt
);