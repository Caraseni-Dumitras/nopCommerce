using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Nop.Services.Configuration;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoService: IProductProviderInfigoService
{
    private readonly ProductProviderInfigoHttpClient _httpClient;
    private readonly ISettingService                 _settingService;

    public ProductProviderInfigoService(ProductProviderInfigoHttpClient httpClient, ISettingService settingService)
    {
        _httpClient          = httpClient;
        _settingService      = settingService;
    }

    public async Task<List<int>> GetAllProducts()
    {
        var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        var url      = settings.BaseApiUrl + settings.GetAllProductsUrl;
        var apiType  = settings.ApiType;
        var apiKey   = settings.ApiKey;

        var data = await _httpClient.RequestAsync(url, apiType, apiKey);

        var productIdList = JsonConvert.DeserializeObject<List<int>>(data);

        return productIdList;
    }

    public Task<ProductModel> GetProductById(int id)
    {
        throw new System.NotImplementedException();
    }
}