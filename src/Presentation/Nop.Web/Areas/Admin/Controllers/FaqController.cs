using Microsoft.AspNetCore.Mvc;
using Nop.Services.FAQs;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Models.FAQs;
using Nop.Web.Framework.Mvc.Filters;

namespace Nop.Web.Areas.Admin.Controllers;

public class FaqController : BaseAdminController
{
    protected readonly IPermissionService _permissionService;
    protected readonly IFaqModelFactory   _faqModelFactory;
    protected readonly IFaqService        _faqService;

    public FaqController(IPermissionService permissionService, IFaqModelFactory faqModelFactory, IFaqService faqService)
    {
        _permissionService = permissionService;
        _faqModelFactory   = faqModelFactory;
        _faqService   = faqService;
    }

    public virtual IActionResult Index()
    {
        return RedirectToAction("List");
    }

    public virtual async Task<IActionResult> List()
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageFaq))
            return AccessDeniedView();

        var model = await _faqModelFactory.PrepareFaqSearchModelAsync(new FaqSearchModel());
        
        return View(model);
    }
    
    [HttpPost]
    public virtual async Task<IActionResult> List(FaqSearchModel searchModel)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageFaq))
            return await AccessDeniedDataTablesJson();

        var model = await _faqModelFactory.PrepareFaqListModelAsync(searchModel);

        return Json(model);
    }
    
    public virtual async Task<IActionResult> Edit(int id)
    {
        if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageForums))
            return AccessDeniedView();

        var faq = await _faqService.GetFaqByIdAsync(id);
        if (faq == null)
            return RedirectToAction("List");

        var model = await _faqModelFactory.PrepareFaqModelAsync(faq);

        return View(model);
    }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(FaqModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageTopics))
                return AccessDeniedView();

            //try to get a topic with the specified id
            var faq = await _faqService.GetFaqByIdAsync(model.Id);
            if (faq == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                faq.QuestionTitle       = model.QuestionTitle;
                faq.QuestionDescription = model.QuestionDescription;
                faq.AnswerTitle         = model.AnswerTitle;
                faq.CategoryId          = model.CategoryId;
                
                await _faqService.UpdateFaqAsync(faq);

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = faq.Id });
            }

            model = await _faqModelFactory.PrepareFaqModelAsync(faq);

            return View(model);
        }
}