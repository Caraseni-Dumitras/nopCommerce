using Nop.Core.Domain.FAQs;
using Nop.Data;

namespace Nop.Services.FAQs;

public class FaqProductService : IFaqProductService
{
    protected readonly IRepository<FaqProductMapping> _faqProductRepository;

    public FaqProductService(IRepository<FaqProductMapping> faqProductRepository)
    {
        _faqProductRepository = faqProductRepository;
    }

    public async Task InsertFaqProductAsync(FaqProductMapping faqProduct)
    {
        await _faqProductRepository.InsertAsync(faqProduct, false);
    }

    public async Task<List<FaqProductMapping>> GetAllFaqByProductsIdAsync(int productId)
    {
        var entities = await _faqProductRepository.Table.Where(f => f.ProductId == productId).ToListAsync();
        return entities;
    }
}