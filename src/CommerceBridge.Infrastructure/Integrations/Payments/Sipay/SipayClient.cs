using System.Net.Http.Json;
using CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;

namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay;

public sealed class SipayClient
{
    private readonly HttpClient _httpClient;

    public SipayClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SipayPaymentResponse> CreatePaymentAsync(
        SipayPaymentRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/payments",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<SipayPaymentResponse>(
                cancellationToken: cancellationToken);

        return result
               ?? throw new InvalidOperationException(
                   "Sipay response could not be parsed.");
    }
}