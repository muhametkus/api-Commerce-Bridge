using System.Text.Json.Serialization;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayInvoiceItem
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("price")]
    public string Price { get; set; } = null!;

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    [JsonPropertyName("description")]
    public string? Description { get; set; }
}