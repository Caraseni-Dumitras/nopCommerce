using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigo : BasePlugin, IMiscPlugin
{
    protected readonly IWebHelper                     _webHelper;
    private readonly   ISettingService                _settingService;
    private readonly   ILocalizationService           _localizationService;
    private readonly   IScheduleTaskService           _scheduleTaskService;
    private readonly   ILogger<ProductProviderInfigo> _logger;
    private readonly   ISpecificationAttributeService _specificationAttributeService;

    public ProductProviderInfigo(IWebHelper                     webHelper,           ISettingService      settingService, 
                                 ILocalizationService           localizationService, IScheduleTaskService scheduleTaskService, 
                                 ILogger<ProductProviderInfigo> logger,              ISpecificationAttributeService specificationAttributeService)
    {
        _webHelper                          = webHelper;
        _settingService                     = settingService;
        _localizationService                = localizationService;
        _scheduleTaskService                = scheduleTaskService;
        _logger                             = logger;
        _specificationAttributeService = specificationAttributeService;
    }

    public override async Task InstallAsync()
    {
        _logger.LogDebug("Installing plugin");
        var apiSettings = new ProductProviderInfigoSettings
        {
            BaseApiUrl               = "",
            ApiKey                   = "",
            GetProductByIdUrl        = "",
            GetAllProductsUrl        = "",
            DefaultProductPictureUrl = ""
        };

        await _settingService.SaveSettingAsync(apiSettings);

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.ProductProvider.Infigo.BaseApiUrl"]        = "Base Api Url",
            ["Plugins.ProductProvider.Infigo.ApiKey"]            = "Api Key",
            ["Plugins.ProductProvider.Infigo.GetProductByIdUrl"] = "Get Product By Id Url",
            ["Plugins.ProductProvider.Infigo.GetAllProductsUrl"] = "Get All Products Url",
            ["Plugins.ProductProvider.Infigo.DefaultProductPictureUrl"] = "Default Product Picture Url"
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

        var specificationAttribute = new SpecificationAttribute() { Name = "ExternalId" };

        await _specificationAttributeService.InsertSpecificationAttributeAsync(specificationAttribute);

        await base.InstallAsync();
    }
    
    public override string GetConfigurationPageUrl()
    {
        _logger.LogDebug("Getting configuration page url");
        return $"{_webHelper.GetStoreLocation()}Admin/ProductProviderInfigo/Configure";
    }

    public override async Task UninstallAsync()
    {
        var productProviderTask = await _scheduleTaskService.GetTaskByTypeAsync(ProductProviderInfigoDefaults.SyncProductsTask.Type);
        if (productProviderTask != null)
        {
            await _scheduleTaskService.DeleteTaskAsync(productProviderTask);
        }
        
        var specificationAttributes = await _specificationAttributeService.GetSpecificationAttributesAsync();
        var specificationAttribute  = specificationAttributes.FirstOrDefault(s => s.Name == "ExternalId");
        if (specificationAttribute != null)
        {
            await _specificationAttributeService.DeleteSpecificationAttributeAsync(specificationAttribute);
        }

        await base.UninstallAsync();
    }
}