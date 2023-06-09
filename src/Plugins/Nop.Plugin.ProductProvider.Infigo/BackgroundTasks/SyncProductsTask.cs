using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Plugin.ProductProvider.Infigo.Services;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo.BackgroundTasks;

public class SyncProductsTask : IScheduleTask
{
    private readonly IProductProviderInfigoService _productProviderInfigoService;
    private readonly ILogger<SyncProductsTask>     _logger;

    public SyncProductsTask(IProductProviderInfigoService productProviderInfigoService, ILogger<SyncProductsTask> logger)
    {
        _productProviderInfigoService = productProviderInfigoService;
        _logger                  = logger;
    }
    
    public async Task ExecuteAsync()
    {
        _logger.LogInformation("Start executing Sync Products Task");
        var productsIds = await _productProviderInfigoService.GetAllProductsIds();

        foreach (var item in productsIds)
        {
            var product = await _productProviderInfigoService.GetProductById(item);

            await _productProviderInfigoService.Insert(product);
        }
    }
}