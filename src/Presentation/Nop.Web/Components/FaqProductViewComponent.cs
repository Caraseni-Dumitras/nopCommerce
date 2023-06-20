using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components;

public class FaqProductViewComponent : NopViewComponent
{
    protected readonly IFaqModelFactory _faqModelFactory;

    public FaqProductViewComponent(IFaqModelFactory faqModelFactory)
    {
        _faqModelFactory = faqModelFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(int productId)
    {
        var model = await _faqModelFactory.PrepareFaqProductIndexModelAsync(productId);
        return View(model);
    }
}