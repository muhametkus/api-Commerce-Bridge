using System.Text.Json.Serialization;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayTokenResponse
{
    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("status_description")]
    public string? StatusDescription { get; set; }

    [JsonPropertyName("data")]
    public SipayTokenData? Data { get; set; }
}

public sealed class SipayTokenData
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;

    [JsonPropertyName("is_3d")]
    public int Is3D { get; set; }

    [JsonPropertyName("expires_at")]
    public string? ExpiresAt { get; set; }
}