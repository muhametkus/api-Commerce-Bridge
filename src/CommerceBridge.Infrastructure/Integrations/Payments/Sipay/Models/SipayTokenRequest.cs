using System.Text.Json.Serialization;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayTokenRequest
{
    [JsonPropertyName("app_id")]
    public string AppId { get; set; } = null!;

    [JsonPropertyName("app_secret")]
    public string AppSecret { get; set; } = null!;

    [JsonPropertyName("app_lang")]
    public string AppLang { get; set; } = "en";
}