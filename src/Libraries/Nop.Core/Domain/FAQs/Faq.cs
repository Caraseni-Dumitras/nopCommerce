namespace Nop.Core.Domain.FAQs;

public class Faq : BaseEntity
{
    public string QuestionTitle       { get; set; }
    public string QuestionDescription { get; set; }
    public string AnswerTitle         { get; set; }
    public string AnswerDescription   { get; set; }
    public int    CategoryId          { get; set; }
}