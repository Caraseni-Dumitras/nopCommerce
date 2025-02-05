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
    public IList<int> CategoryIds { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CategoryName")]
    public List<string> CategoryName { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.Categories")]
    public IList<int> SelectedCategoryIds { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CreatedOnUtc")]
    public DateTime CreatedOnUtc { get;          set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.UpdatedOnUtc")]
    public DateTime UpdatedOnUtc { get;          set; }
    
    public IList<SelectListItem> AvailableCategories { get; set; }
    public int                   ProductId           { get; set; }
    public IList<int>             ProductIds          { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.ProductName")]
    public List<string> ProductName { get; set; }
    public bool IsFaqCategory { get; set; } = false;
}