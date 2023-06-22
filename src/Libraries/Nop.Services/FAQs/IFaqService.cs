using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqService
{
    Task<IList<Faq>> GetAllFaqByCategoryIdAsync(int categoryId);
    Task<List<Faq>> GetAllFaqsAsync(List<int> categoryIds);
    Task<Faq> GetFaqByIdAsync(int id);
    Task UpdateFaqAsync(Faq faq);
    // Task            DeleteFaqAsync(Faq                faq);
    // Task            InsertFaqAsync(Faq                faq);
    // Task<IList<Faq>> GetAllFaqByIdsAsync(List<int> ids);
    Task<IList<Faq>>                GetAllFaqByProductsIdAsync(int                 productId);
    Task<IList<Category>>           GetFaqCategoriesAsync(int                      id);
    Task<IList<Product>>            GetFaqProductsAsync(int                        id);
    Task<bool>                      CheckIsFaqCategoryAsync(int                    id);
    Task<bool>                      UpdateFaqCategoryAsync(ICollection<int>        categoriesIds, int faqId);
    Task<IList<FaqCategoryMapping>> GetAllFaqCategoryEntitiesAsync(int             id);
    Task                            DeleteFaqCategoryAsync(FaqCategoryMapping      faqCategoryMapping);
    Task<FaqCategoryMapping>        FindFaqCategoryAsync(IList<FaqCategoryMapping> source, int faqId, int categoryId);
    Task                            InsertFaqCategoryAsync(FaqCategoryMapping      faqCategoryMapping);
    Task                            InsertFaqProductAsync(FaqProductMapping        faqProductMapping);
    Task<IList<FaqProductMapping>>  GetAllFaqProductEntitiesAsync(int              id);
    Task<FaqProductMapping>         FindFaqProductAsync(IList<FaqProductMapping>   source, int faqId, int productId);
    Task                            DeleteFaqProductAsync(FaqProductMapping      faqProductMapping);
}