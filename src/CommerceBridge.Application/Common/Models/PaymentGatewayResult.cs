namespace CommerceBridge.Application.Common.Models;

public sealed record PaymentGatewayResult(
    bool IsSuccessful,
    string? ProviderOrderId,
    string? PaymentUrl,
    string? ErrorMessage
);