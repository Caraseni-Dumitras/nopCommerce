using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Areas.Admin.Models.FAQs;

public record FaqModel : BaseNopEntityModel
{
    public FaqModel()
    {
        SelectedCategoryIds = new List<int>();
        AvailableCategories = new List<SelectListItem>();
    }
    
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.QuestionTitle")]
    public string QuestionTitle { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.QuestionDescription")]
    public string QuestionDescription { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.AnswerTitle")]
    public string AnswerTitle { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.AnswerDescription")]
    public string AnswerDescription { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CategoryId")]
    public int CategoryId { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CategoryName")]
    public string CategoryName { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.Categories")]
    public IList<int> SelectedCategoryIds { get;            set; }
    public IList<SelectListItem> AvailableCategories { get; set; }
    public int                   ProductId           { get; set; }
}