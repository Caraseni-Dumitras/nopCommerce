using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqService
{
    Task<List<Faq>> GetAllFaqByCategoryIdAsync(int categoryId);
}