namespace Nop.Core.Domain.FAQs;

public class Faq : BaseEntity
{
    public Faq()
    {
        CreatedOnUtc = UpdatedOnUtc = DateTime.UtcNow;
    }
    
    public string   QuestionTitle       { get; set; }
    public string   QuestionDescription { get; set; }
    public string   AnswerTitle         { get; set; }
    public string   AnswerDescription   { get; set; }
    public DateTime CreatedOnUtc        { get; set; }
    public DateTime UpdatedOnUtc        { get; set; }
}