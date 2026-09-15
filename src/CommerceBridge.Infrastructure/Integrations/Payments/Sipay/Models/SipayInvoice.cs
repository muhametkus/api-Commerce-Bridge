using System.Text.Json.Serialization;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

public sealed class SipayInvoice
{
    [JsonPropertyName("invoice_id")]
    public string InvoiceId { get; set; } = null!;

    [JsonPropertyName("invoice_description")]
    public string InvoiceDescription { get; set; } = null!;

    [JsonPropertyName("total")]
    public string Total { get; set; } = null!;

    [JsonPropertyName("discount")]
    public decimal Discount { get; set; }

    [JsonPropertyName("return_url")]
    public string ReturnUrl { get; set; } = null!;

    [JsonPropertyName("cancel_url")]
    public string CancelUrl { get; set; } = null!;

    [JsonPropertyName("response_method")]
    public string ResponseMethod { get; set; } = "POST";

    [JsonPropertyName("bill_email")]
    public string BillEmail { get; set; } = null!;

    [JsonPropertyName("items")]
    public List<SipayInvoiceItem> Items { get; set; } = [];
}