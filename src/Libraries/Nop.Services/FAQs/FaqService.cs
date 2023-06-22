using LinqToDB;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.FAQs;
using Nop.Data;
using Nop.Services.Catalog;

namespace Nop.Services.FAQs;

public class FaqService : IFaqService
{
    protected readonly IRepository<Faq>                _faqRepository;
    protected readonly IRepository<FaqCategoryMapping> _faqCategoryMappingRepository;
    protected readonly IRepository<FaqProductMapping>  _faqProductMappingRepository;
    protected readonly ICategoryService                _categoryService;
    protected readonly IProductService                 _productService;

    public FaqService(IRepository<Faq> faqRepository, IRepository<FaqCategoryMapping> faqCategoryMappingRepository, IRepository<FaqProductMapping> faqProductMappingRepository, ICategoryService categoryService, IProductService productService)
    {
        _faqRepository                = faqRepository;
        _faqCategoryMappingRepository = faqCategoryMappingRepository;
        _faqProductMappingRepository  = faqProductMappingRepository;
        _categoryService              = categoryService;
        _productService          = productService;
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

    public async Task<IList<Category>> GetFaqCategoriesAsync(int id)
    {
        var categoryIds = await _faqCategoryMappingRepository.Table.Where(it=> it.FaqId == id).Select(it => it.CategoryId).ToArrayAsync();

        return await _categoryService.GetCategoriesByIdsAsync(categoryIds);
    }

    public async Task<IList<Product>> GetFaqProductsAsync(int id)
    {
        var productIds = await _faqProductMappingRepository.Table.Where(it=> it.FaqId == id).Select(it => it.ProductId).ToArrayAsync();

        return await _productService.GetProductsByIdsAsync(productIds);
    }

    public async Task<bool> CheckIsFaqCategoryAsync(int id)
    {
        return !_faqProductMappingRepository.Table.Any(it => it.FaqId == id);
    }

    public Task<bool> UpdateFaqCategoryAsync(ICollection<int> categoriesIds, int faqId)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<FaqCategoryMapping>> GetAllFaqCategoryEntitiesAsync(int id)
    {
        return await _faqCategoryMappingRepository.Table.Where(it => it.FaqId == id).ToListAsync();
    }

    public async Task DeleteFaqCategoryAsync(FaqCategoryMapping faqCategoryMapping)
    {
        await _faqCategoryMappingRepository.DeleteAsync(faqCategoryMapping, false);
    }

    public async Task<FaqCategoryMapping> FindFaqCategoryAsync(IList<FaqCategoryMapping> source, int faqId, int categoryId)
    {
        foreach (var faqCategory in source)
            if (faqCategory.FaqId == faqId && faqCategory.CategoryId == categoryId)
                return faqCategory;

        return null;
    }

    public async Task InsertFaqCategoryAsync(FaqCategoryMapping faqCategoryMapping)
    {
        await _faqCategoryMappingRepository.InsertAsync(faqCategoryMapping, false);
    }

    public async Task InsertFaqProductAsync(FaqProductMapping faqProductMapping)
    {
        await _faqProductMappingRepository.InsertAsync(faqProductMapping, false);
    }

    public async Task<IList<FaqProductMapping>> GetAllFaqProductEntitiesAsync(int id)
    {
        return await _faqProductMappingRepository.Table.Where(it => it.FaqId == id).ToListAsync();
    }

    public async Task<FaqProductMapping> FindFaqProductAsync(IList<FaqProductMapping> source, int faqId, int productId)
    {
        foreach (var faqProduct in source)
            if (faqProduct.FaqId == faqId && faqProduct.ProductId == productId)
                return faqProduct;

        return null;
    }

    public async  Task DeleteFaqProductAsync(FaqProductMapping faqProductMapping)
    {
        await _faqProductMappingRepository.DeleteAsync(faqProductMapping, false);
    }

    public async Task<List<Faq>> GetAllFaqsAsync(List<int> categoryIds)
    {
        if (categoryIds.Any())
        {
            var entities = new List<Faq>();
            foreach (var categoryId in categoryIds)
            {
                entities.AddRange(await GetAllFaqByCategoryIdAsync(categoryId));
            }
            return entities;
        }
        return await _faqRepository.Table.ToListAsync();
    }
    
    public async Task<Faq> GetFaqByIdAsync(int id)
    {
        return await _faqRepository.GetByIdAsync(id);
    }
    
    public async Task UpdateFaqAsync(Faq faq)
    {
        faq.UpdatedOnUtc = DateTime.UtcNow;
        await _faqRepository.UpdateAsync(faq);
    }
    
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