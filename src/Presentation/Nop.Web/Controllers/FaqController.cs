using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.FAQs;

namespace Nop.Web.Controllers;

[AutoValidateAntiforgeryToken]
public class FaqController : BasePublicController
{
    public virtual IActionResult Index(int categoryId = 0, string categoryName = "Generic")
    {
        var model = new FaqModel() { CategoryId = categoryId , CategoryName = categoryName};
        return View("~/Views/FAQS/Index.cshtml", model);
    }
}