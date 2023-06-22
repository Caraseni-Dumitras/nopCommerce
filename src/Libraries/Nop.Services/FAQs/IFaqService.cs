using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqService
{
    Task<IList<Faq>> GetAllFaqByCategoryIdAsync(int    categoryId);
    // Task<List<Faq>> GetAllFaqsAsync(List<int>         categoryIds);
    // Task<Faq>       GetFaqByIdAsync(int               id);
    // Task            UpdateFaqAsync(Faq                faq);
    // Task            DeleteFaqAsync(Faq                faq);
    // Task            InsertFaqAsync(Faq                faq);
    // Task<IList<Faq>> GetAllFaqByIdsAsync(List<int> ids);
}