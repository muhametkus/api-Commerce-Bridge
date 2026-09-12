namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayPaymentRequest
{
    public string OrderId { get; set; } = null!;

    public decimal Amount { get; set; }
}