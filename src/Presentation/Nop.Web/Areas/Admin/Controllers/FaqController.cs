using Microsoft.AspNetCore.Mvc;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Models.FAQs;

namespace Nop.Web.Areas.Admin.Controllers;

public class FaqController : BaseAdminController
{
    protected readonly IPermissionService _permissionService;

    public FaqController(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    public virtual IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public virtual async Task<IActionResult> List()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageForums))
            return AccessDeniedView();

        var model = new FaqSearchModel()
        {
            Id = 0
        };
        
        return View(model);
    }
}