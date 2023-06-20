namespace Nop.Core.Domain.FAQs;

public class FaqProductMapping : BaseEntity
{
    public int FaqId     { get; set; }
    public int ProductId { get; set; }
}