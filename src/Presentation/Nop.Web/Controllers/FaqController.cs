using Microsoft.AspNetCore.Mvc;
using Nop.Web.Models.FAQs;

namespace Nop.Web.Controllers;

[AutoValidateAntiforgeryToken]
public class FaqController : BasePublicController
{
    public virtual IActionResult Index()
    {
        var model = new FaqModel();
        return View("~/Views/FAQS/Index.cshtml", model);
    }
}