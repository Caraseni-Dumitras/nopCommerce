using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigo : BasePlugin, IMiscPlugin
{
    protected readonly IWebHelper           _webHelper;
    private readonly   ISettingService      _settingService;
    private readonly   ILocalizationService _localizationService;
    private readonly   IScheduleTaskService        _scheduleTaskService;

    public ProductProviderInfigo(IWebHelper webHelper, ISettingService settingService, 
                                 ILocalizationService localizationService, IScheduleTaskService scheduleTaskService)
    {
        _webHelper           = webHelper;
        _settingService      = settingService;
        _localizationService = localizationService;
        _scheduleTaskService = scheduleTaskService;
    }

    public override async Task InstallAsync()
    {
        var apiSettings = new ProductProviderInfigoSettings
        {
            BaseApiUrl = "",
            ApiType = "",
            ApiKey = "",
            GetProductByIdUrl ="",
            GetAllProductsUrl = ""
        };

        await _settingService.SaveSettingAsync(apiSettings);

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.ProductProvider.Infigo.BaseApiUrl"]        = "Base Api Url",
            ["Plugins.ProductProvider.Infigo.ApiType"]           = "Api Type",
            ["Plugins.ProductProvider.Infigo.ApiKey"]            = "Api Key",
            ["Plugins.ProductProvider.Infigo.GetProductByIdUrl"] = "Get Product By Id Url",
            ["Plugins.ProductProvider.Infigo.GetAllProductsUrl"] = "Get All Products Url"
        });
        
        if (await _scheduleTaskService.GetTaskByTypeAsync(ProductProviderInfigoDefaults.SyncProductsTask.Type) is null)
        {
            await _scheduleTaskService.InsertTaskAsync(new()
            {
                Enabled        = true,
                LastEnabledUtc = DateTime.UtcNow,
                Seconds        = 86400,
                Name           = ProductProviderInfigoDefaults.SyncProductsTask.Name,
                Type           = ProductProviderInfigoDefaults.SyncProductsTask.Type
            });
        }

        await base.InstallAsync();
    }
    
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/ProductProviderInfigo/Configure";
    }
}