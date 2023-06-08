using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoService: IProductProviderInfigoService
{
    private readonly ProductProviderInfigoHttpClient _httpClient;

    public ProductProviderInfigoService(ProductProviderInfigoHttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task<List<int>> GetAllProducts()
    {
        throw new System.NotImplementedException();
    }

    public Task<ProductModel> GetProductBuId(int id)
    {
        throw new System.NotImplementedException();
    }
}