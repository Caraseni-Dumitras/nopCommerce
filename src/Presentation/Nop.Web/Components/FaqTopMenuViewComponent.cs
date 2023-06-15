using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components;

public class FaqTopMenuViewComponent : NopViewComponent
{
    protected readonly ICatalogModelFactory _catalogModelFactory;

    public FaqTopMenuViewComponent(ICatalogModelFactory catalogModelFactory)
    {
        _catalogModelFactory = catalogModelFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync(int? productThumbPictureSize)
    {
        var model = await _catalogModelFactory.PrepareFaqTopMenuModelAsync();
        return View(model);
    }
}