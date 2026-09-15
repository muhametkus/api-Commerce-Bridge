namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayPurchaseLinkRequest
{
    public string MerchantKey { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string CurrencyCode { get; set; } = "TRY";

    public string Invoice { get; set; } = null!;
}