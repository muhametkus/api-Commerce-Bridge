using System.Text.Json.Serialization;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayPurchaseLinkResponse
{
    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("status_code")]
    public int StatusCode { get; set; }

    [JsonPropertyName("success_message")]
    public string? SuccessMessage { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }

    [JsonPropertyName("order_id")]
    public string? OrderId { get; set; }
}