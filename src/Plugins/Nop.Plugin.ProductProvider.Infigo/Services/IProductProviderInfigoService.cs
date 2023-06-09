using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.ProductProvider.Infigo.Models;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public interface IProductProviderInfigoService
{
    public Task<List<int>>       GetAllProductsIds();
    public Task<ApiProductModel> GetProductById(int     id);
    public Task                  Insert(ApiProductModel model);
}