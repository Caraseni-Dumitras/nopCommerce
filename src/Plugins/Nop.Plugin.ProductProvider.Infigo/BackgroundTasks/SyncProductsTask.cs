using System.Threading.Tasks;
using Nop.Plugin.ProductProvider.Infigo.Services;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo.BackgroundTasks;

public class SyncProductsTask : IScheduleTask
{
    private readonly IProductProviderInfigoService   _productProviderInfigoService;
    
    public SyncProductsTask(IProductProviderInfigoService productProviderInfigoService)
    {
        _productProviderInfigoService = productProviderInfigoService;
    }
    
    public async Task ExecuteAsync()
    {
        var products = await _productProviderInfigoService.GetAllProducts();

        foreach (var item in products)
        {
            var product = await _productProviderInfigoService.GetProductById(item);

            await _productProviderInfigoService.Insert(product);
        }
    }
}