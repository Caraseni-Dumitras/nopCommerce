using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoHttpClient
{
    private readonly HttpClient _httpClient;

    public ProductProviderInfigoHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> RequestAsync(string url, string apiType, string apiKey)
    {
        try
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(apiType, apiKey);

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception e)
        {
            throw e.InnerException;
        }
    }
}