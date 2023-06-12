using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Plugin.ProductProvider.Infigo.Services;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo.BackgroundTasks;

public class SyncProductsTask : IScheduleTask
{
    private readonly IProductProviderInfigoService   _productProviderInfigoService;
    private readonly ILogger<SyncProductsTask>       _logger;
    private readonly ProductProviderInfigoHttpClient _httpClient;

    public SyncProductsTask(IProductProviderInfigoService productProviderInfigoService, ILogger<SyncProductsTask> logger, ProductProviderInfigoHttpClient httpClient)
    {
        _productProviderInfigoService = productProviderInfigoService;
        _logger                       = logger;
        _httpClient              = httpClient;
    }
    
    public async Task ExecuteAsync()
    {
        _logger.LogInformation("Start executing Sync Products Task");
        var productsIds = await _httpClient.RequestAllProductsIdsAsync();

        foreach (var item in productsIds)
        {
            var product = await _httpClient.RequestProductByIdAsync(item);

            await _productProviderInfigoService.Insert(product);
        }
    }
}