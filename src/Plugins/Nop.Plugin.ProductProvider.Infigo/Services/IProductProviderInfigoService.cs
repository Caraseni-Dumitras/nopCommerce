using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public interface IProductProviderInfigoService
{
    public Task<List<int>>    GetAllProducts();
    public Task<ProductModel> GetProductBuId(int id);
}