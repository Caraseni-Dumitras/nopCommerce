namespace Nop.Core.Domain.FAQs;

public class FaqCategoryMapping : BaseEntity
{
    public int FaqId     { get; set; }
    public int CategoryId { get; set; }
}