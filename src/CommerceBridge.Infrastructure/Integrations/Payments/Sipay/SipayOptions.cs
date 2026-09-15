namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay;

public sealed class SipayOptions
{
    public const string SectionName = "Sipay";

    public string BaseUrl { get; init; } = null!;

    public string AppId { get; init; } = null!;

    public string AppSecret { get; init; } = null!;

    public string MerchantKey { get; init; } = null!;

    public string ReturnUrl { get; init; } = null!;

    public string CancelUrl { get; init; } = null!;
}