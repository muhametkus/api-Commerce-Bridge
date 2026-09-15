using System.Net.Http.Headers;
using System.Net.Http.Json;
using CommerceBridge.Infrastructure.Integrations.Payments.Sipay.Models;
using Microsoft.Extensions.Options;
namespace CommerceBridge.Infrastructure.Integrations.Payments.Sipay;

public sealed class SipayClient
{
    private readonly HttpClient _httpClient;
    private readonly SipayOptions _options;

    public SipayClient(
        HttpClient httpClient,
        IOptions<SipayOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> GetTokenAsync(
        CancellationToken cancellationToken)
    {
        var request = new SipayTokenRequest
        {
            AppId = _options.AppId,
            AppSecret = _options.AppSecret,
            AppLang = "en"
        };

        using var response =
            await _httpClient.PostAsJsonAsync(
                "/ccpayment/api/token",
                request,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<SipayTokenResponse>(
                cancellationToken: cancellationToken);

        if (result?.Data?.Token is null)
            throw new InvalidOperationException(
                result?.StatusDescription
                ?? "Sipay token could not be generated.");

        return result.Data.Token;
    }

    public async Task<SipayPurchaseLinkResponse> CreatePurchaseLinkAsync(
        SipayPurchaseLinkRequest request,
        CancellationToken cancellationToken)
    {
        var token = await GetTokenAsync(
            cancellationToken);

        using var httpRequest =
            new HttpRequestMessage(
                HttpMethod.Post,
                "/ccpayment/purchase/link");

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        httpRequest.Content =
            JsonContent.Create(request);

        using var response =
            await _httpClient.SendAsync(
                httpRequest,
                cancellationToken);

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<SipayPurchaseLinkResponse>(
                cancellationToken: cancellationToken);

        return result
            ?? throw new InvalidOperationException(
                "Sipay purchase link response could not be parsed.");
    }
}