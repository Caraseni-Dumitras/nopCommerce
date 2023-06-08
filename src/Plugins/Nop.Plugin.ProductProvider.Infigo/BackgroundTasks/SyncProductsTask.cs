using System.Threading.Tasks;
using Nop.Plugin.ProductProvider.Infigo.Services;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo.BackgroundTasks;

public class SyncProductsTask : IScheduleTask
{
    private readonly ProductProviderInfigoHttpClient _httpClient;
    private readonly IScheduleTaskService            _scheduleTaskService;
    
    public SyncProductsTask(ProductProviderInfigoHttpClient storeHttpClient, IScheduleTaskService scheduleTaskService)
    {
        _httpClient          = storeHttpClient;
        _scheduleTaskService = scheduleTaskService;
    }
    
    public async Task ExecuteAsync()
    {
        await _httpClient.RequestAsync();
    }
}