namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayPaymentResponse
{
    public bool Success { get; set; }

    public string? TransactionId { get; set; }

    public string? Message { get; set; }
}