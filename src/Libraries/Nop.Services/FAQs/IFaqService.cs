using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqService
{
    Task<List<Faq>> GetAllFaqByCategoryIdAsync(int categoryId);
    Task<List<Faq>> GetAllFaqsAsync();
    Task<Faq>       GetFaqByIdAsync(int id);
    Task            UpdateFaqAsync(Faq  faq);
    Task            DeleteFaqAsync(Faq  faq);
    Task            InsertFaqAsync(Faq  faq);
}