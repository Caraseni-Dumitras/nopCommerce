using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.FAQs;

public record FaqSearchModel : BaseSearchModel
{
    public FaqSearchModel()
    {
        AvailableCategories = new List<SelectListItem>();
    }
    
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.SearchCategory")]
    public int SearchCategoryId { get; set; }

    [NopResourceDisplayName("Admin.ContentManagement.FAQ.SearchIncludeSubCategories")]
    public bool SearchIncludeSubCategories { get;           set; }
    public IList<SelectListItem> AvailableCategories { get; set; }
    public int SearchProductId     { get; set; }
    public bool SearchByProductId   { get; set; }
}