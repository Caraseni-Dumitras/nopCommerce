using Nop.Core.Domain.FAQs;
using Nop.Data;

namespace Nop.Services.FAQs;

public class FaqService : IFaqService
{
    protected readonly IRepository<Faq>                _faqRepository;
    protected readonly IRepository<FaqCategoryMapping> _faqCategoryMappingRepository;
    protected readonly IRepository<FaqProductMapping> _faqProductMappingRepository;

    public FaqService(IRepository<Faq> faqRepository, IRepository<FaqCategoryMapping> faqCategoryMappingRepository, IRepository<FaqProductMapping> faqProductMappingRepository)
    {
        _faqRepository                    = faqRepository;
        _faqCategoryMappingRepository     = faqCategoryMappingRepository;
        _faqProductMappingRepository      = faqProductMappingRepository;
    }

    public async Task<IList<Faq>> GetAllFaqByCategoryIdAsync(int categoryId)
    {
        var entitiesIds = await _faqCategoryMappingRepository.Table
                                                             .Where(it => it.CategoryId == categoryId)
                                                             .Select(it => it.FaqId).ToListAsync();
        
        return await _faqRepository.GetByIdsAsync(entitiesIds);
    }

    public async Task<IList<Faq>> GetAllFaqByProductsIdAsync(int productId)
    {
        var entitiesIds = await _faqProductMappingRepository.Table
                                                             .Where(it => it.ProductId == productId)
                                                             .Select(it => it.FaqId).ToListAsync();
        return await _faqRepository.GetByIdsAsync(entitiesIds);
    }

    // public async Task<List<Faq>> GetAllFaqsAsync(List<int> categoryIds)
    // {
    //     if (categoryIds.Any())
    //     {
    //         return await _faqRepository.Table.Where(f => categoryIds.Contains(f.CategoryId)).ToListAsync();
    //     }
    //     return await _faqRepository.Table.ToListAsync();
    // }
    //
    // public async Task<Faq> GetFaqByIdAsync(int id)
    // {
    //     return await _faqRepository.GetByIdAsync(id);
    // }
    //
    // public async Task UpdateFaqAsync(Faq faq)
    // {
    //     await _faqRepository.UpdateAsync(faq);
    // }
    //
    // public async Task DeleteFaqAsync(Faq faq)
    // {
    //     await _faqRepository.DeleteAsync(faq, false);
    // }
    //
    // public async Task InsertFaqAsync(Faq faq)
    // {
    //     await _faqRepository.InsertAsync(faq, false);
    // }
    //
    // public async Task<IList<Faq>> GetAllFaqByIdsAsync(List<int> ids)
    // {
    //     return await _faqRepository.GetByIdsAsync(ids);
    // }
}