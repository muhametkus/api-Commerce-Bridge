namespace CommerceBridge.Application.Common.Models;

public sealed record PaymentGatewayResult(
    bool IsSuccessful,
    string? TransactionId,
    string? ErrorMessage
);