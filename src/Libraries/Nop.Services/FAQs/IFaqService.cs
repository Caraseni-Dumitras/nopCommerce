using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqService
{
    Task<IList<Faq>>                GetAllFaqByCategoryIdAsync(int                 categoryId);
    Task<List<Faq>>                 GetAllFaqsAsync(List<int>                      categoryIds);
    Task<Faq>                       GetFaqByIdAsync(int                            id);
    Task                            UpdateFaqAsync(Faq                             faq);
    Task                            DeleteFaqAsync(Faq                             faq);
    Task                            InsertFaqAsync(Faq                             faq);
    Task<IList<Faq>>                GetAllFaqByProductsIdAsync(int                 productId);
    Task<IList<Category>>           GetFaqCategoriesAsync(int                      id);
    Task<IList<Product>>            GetFaqProductsAsync(int                        id);
    Task<bool>                      CheckIsFaqCategoryAsync(int                    id);
    Task<IList<FaqCategoryMapping>> GetAllFaqCategoryEntitiesByFaqIdAsync(int      id);
    Task                            DeleteFaqCategoryAsync(FaqCategoryMapping      faqCategoryMapping);
    Task<FaqCategoryMapping>        FindFaqCategoryAsync(IList<FaqCategoryMapping> source, int faqId, int categoryId);
    Task                            InsertFaqCategoryAsync(FaqCategoryMapping      faqCategoryMapping);
    Task                            InsertFaqProductAsync(FaqProductMapping        faqProductMapping);
    Task<IList<FaqProductMapping>>  GetAllFaqProductEntitiesByFaqIdAsync(int       id);
    Task<FaqProductMapping>         FindFaqProductAsync(IList<FaqProductMapping>   source, int faqId, int productId);
    Task                            DeleteFaqProductAsync(FaqProductMapping        faqProductMapping);
    Task                            DeleteFaqProductsAsync(int                     id);
    Task                            DeleteFaqCategoriesAsync(int                   id);
    Task<int>                       GetProductIdByFaqId(int                        id);
}