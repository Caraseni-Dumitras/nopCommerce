using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.ProductProvider.Infigo.Models;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public interface IProductProviderInfigoService
{
    public Task                  Insert(ApiProductModel model);
}