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
}