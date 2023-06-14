using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Web.Models.FAQs;

public record FaqModel : BaseNopEntityModel
{
    [NopResourceDisplayName("Admin.FAQs.Faq.Fields.QuestionTitle")]
    public string QuestionTitle       { get; set; }
    [NopResourceDisplayName("Admin.FAQs.Faq.Fields.QuestionDescription")]
    public string QuestionDescription { get; set; }
    [NopResourceDisplayName("Admin.FAQs.Faq.Fields.AnswerTitle")]
    public string AnswerTitle         { get; set; }
    [NopResourceDisplayName("Admin.FAQs.Faq.Fields.AnswerDescription")]
    public string AnswerDescription   { get; set; }
    [NopResourceDisplayName("Admin.FAQs.Faq.Fields.CategoryId")]
    public int    CategoryId          { get; set; }
}