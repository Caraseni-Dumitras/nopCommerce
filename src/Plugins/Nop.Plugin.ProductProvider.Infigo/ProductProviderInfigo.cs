using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Common;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;

namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigo : BasePlugin, IMiscPlugin
{
    protected readonly IWebHelper           _webHelper;
    private readonly   ISettingService      _settingService;
    private readonly   ILocalizationService _localizationService;

    public ProductProviderInfigo(IWebHelper webHelper, ISettingService settingService, ILocalizationService localizationService)
    {
        _webHelper                = webHelper;
        _settingService           = settingService;
        _localizationService = localizationService;
    }

    public override async Task InstallAsync()
    {
        var apiSettings = new ProductProviderInfigoSettings
        {
            ExternalApiUrl = ""
        };

        await _settingService.SaveSettingAsync(apiSettings);

        await _localizationService.AddOrUpdateLocaleResourceAsync(new Dictionary<string, string>
        {
            ["Plugins.ProductProvider.Infigo.ExternalApiUrl"] = "External Api Url"
        });

        await base.InstallAsync();
    }
    
    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/ProductProviderInfigo/Configure";
    }
}