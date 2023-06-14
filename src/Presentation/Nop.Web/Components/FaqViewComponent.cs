using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components;

public class FaqViewComponent : NopViewComponent
{
    protected readonly IFaqModelFactory _faqModelFactory;

    public FaqViewComponent(IFaqModelFactory faqModelFactory)
    {
        _faqModelFactory = faqModelFactory;
    }
    
    public async Task<IViewComponentResult> InvokeAsync(int categoryId = 0)
    {
        var model = await _faqModelFactory.PrepareFaqIndexModelAsync(categoryId);
        return View(model);
    }
}