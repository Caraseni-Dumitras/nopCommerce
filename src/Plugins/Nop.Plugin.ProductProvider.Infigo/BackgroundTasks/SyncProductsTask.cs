using System.Threading.Tasks;
using Nop.Plugin.ProductProvider.Infigo.Services;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo.BackgroundTasks;

public class SyncProductsTask : IScheduleTask
{
    private readonly ProductProviderInfigoHttpClient _httpClient;
    private readonly IScheduleTaskService            _scheduleTaskService;
    private readonly IProductProviderInfigoService   _productProviderInfigoService;
    
    public SyncProductsTask(ProductProviderInfigoHttpClient storeHttpClient, IScheduleTaskService scheduleTaskService, 
                            IProductProviderInfigoService productProviderInfigoService)
    {
        _httpClient                        = storeHttpClient;
        _scheduleTaskService               = scheduleTaskService;
        _productProviderInfigoService      = productProviderInfigoService;
    }
    
    public async Task ExecuteAsync()
    {
        await _productProviderInfigoService.GetAllProducts();
    }
}