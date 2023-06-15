using Nop.Core.Domain.FAQs;
using Nop.Data;

namespace Nop.Services.FAQs;

public class FaqService : IFaqService
{
    protected readonly IRepository<Faq> _faqRepository;

    public FaqService(IRepository<Faq> faqRepository)
    {
        _faqRepository = faqRepository;
    }

    public async Task<List<Faq>> GetAllFaqByCategoryIdAsync(int categoryId)
    {
        var entities = await _faqRepository.Table.Where(f => f.CategoryId == categoryId).ToListAsync();
        return entities;
    }
}