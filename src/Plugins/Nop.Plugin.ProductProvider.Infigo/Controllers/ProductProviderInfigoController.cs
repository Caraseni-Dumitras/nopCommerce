using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.ProductProvider.Infigo.Models;
using Nop.Services.Configuration;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.ProductProvider.Infigo.Controllers;

[AuthorizeAdmin]
[Area(AreaNames.Admin)]
public class ProductProviderInfigoController : BasePluginController
{
    protected readonly ISettingService _settingService;

    public ProductProviderInfigoController(ISettingService settingService)
    {
        _settingService = settingService;
    }
    
    public async Task<IActionResult> Configure()
    {
        var apiSettings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        
        var model = new ConfigurationModel()
        {
            ExternalApiUrl = apiSettings.ExternalApiUrl
        };
        
        return View("~/Plugins/ProductProvider.Infigo/Views/Configure.cshtml", model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Configure(ConfigurationModel model)
    {
        var apiSettings = new ProductProviderInfigoSettings()
        {
            ExternalApiUrl = model.ExternalApiUrl
        };

        await _settingService.SaveSettingAsync(apiSettings);

        return await Configure();
    }
}