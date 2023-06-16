using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.Areas.Admin.Controllers;

public class FaqController : BaseAdminController
{
    // GET
    public virtual IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public virtual async Task<IActionResult> List()
    {
        return RedirectToAction("List", "Forum");
    }
}