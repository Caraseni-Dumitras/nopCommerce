using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Models.FAQs;

public record FaqModel : BaseNopEntityModel
{
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.QuestionTitle")]
    public string QuestionTitle       { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields..QuestionDescription")]
    public string QuestionDescription { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.AnswerTitle")]
    public string AnswerTitle         { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.AnswerDescription")]
    public string AnswerDescription   { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CategoryId")]
    public int    CategoryId          { get; set; }
    [NopResourceDisplayName("Admin.ContentManagement.FAQ.Fields.CategoryName")]
    public string CategoryName { get; set; }
}