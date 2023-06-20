using Nop.Web.Models.FAQs;

namespace Nop.Web.Factories;

public interface IFaqModelFactory
{
    Task<FaqIndexModel> PrepareFaqIndexModelAsync(int categoryId);
    Task<FaqIndexModel> PrepareFaqProductIndexModelAsync(int productId);
}