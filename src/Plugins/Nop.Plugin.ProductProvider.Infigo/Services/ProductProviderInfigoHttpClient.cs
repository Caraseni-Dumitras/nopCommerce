using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Services.Configuration;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoHttpClient
{
    private readonly HttpClient                               _httpClient;
    private readonly ISettingService                          _settingService;
    private readonly ILogger<ProductProviderInfigoHttpClient> _logger;
    private readonly string                                   _apiType = "Basic";

    public ProductProviderInfigoHttpClient(HttpClient httpClient, ISettingService settingService, ILogger<ProductProviderInfigoHttpClient> logger)
    {
        _httpClient     = httpClient;
        _settingService = settingService;
        _logger         = logger;
    }

    public async Task<string> RequestAllProductsIdsAsync()
    {
        try
        {
            var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
            var url      = settings.BaseApiUrl + settings.GetAllProductsUrl;
            var apiKey   = settings.ApiKey;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiType, apiKey);

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception)
        {
            _logger.LogError("Failed to get products");
            throw;
        }
    }
    
    public async Task<string> RequestProductByIdAsync(int id)
    {
        try
        {
            var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
            var url      = settings.BaseApiUrl + settings.GetProductByIdUrl + id;
            var apiKey   = settings.ApiKey;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiType, apiKey);

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        catch (Exception)
        {
            _logger.LogError("Failed to get product with id {Id}", id);
            throw;
        }
    }
    
    public async Task<byte[]> RequestPicturesAsync(string url)
    {
        try
        {
            var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
            var apiKey= settings.ApiKey;
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiType, apiKey);

            var response = await _httpClient.GetAsync(url);

            var responseBody = await response.Content.ReadAsByteArrayAsync();
            return responseBody;
        }
        catch (Exception)
        {
            _logger.LogError("Failed to get product picture");
            throw;
        }
    }
}