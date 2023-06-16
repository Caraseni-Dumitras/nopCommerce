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

    public async Task<List<Faq>> GetAllFaqsAsync()
    {
        return await _faqRepository.Table.ToListAsync();
    }

    public async Task<Faq> GetFaqByIdAsync(int id)
    {
        return await _faqRepository.GetByIdAsync(id);
    }

    public async Task UpdateFaqAsync(Faq faq)
    {
        await _faqRepository.UpdateAsync(faq);
    }
}