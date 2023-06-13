using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.Controllers;

[AutoValidateAntiforgeryToken]
public class FaqController : BasePublicController
{
    public virtual IActionResult Index()
    {
        return View("~/Views/FAQS/Index.cshtml");
    }
}