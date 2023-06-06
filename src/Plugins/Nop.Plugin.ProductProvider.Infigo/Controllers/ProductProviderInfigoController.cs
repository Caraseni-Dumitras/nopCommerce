using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.ProductProvider.Infigo.Models;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Plugin.ProductProvider.Infigo.Controllers;

[AutoValidateAntiforgeryToken]
[AuthorizeAdmin]
[Area(AreaNames.Admin)]
public class ProductProviderInfigoController : BasePluginController
{
    public async Task<IActionResult> Configure()
    {
        var model = new ConfigurationModel() { Id = 1, Name = "ConfigurationTestName" };
        return View("~/Plugins/ProductProvider.Infigo/Views/Configure.cshtml", model);
    }
}