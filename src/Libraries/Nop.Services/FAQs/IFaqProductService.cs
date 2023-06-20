using Nop.Core.Domain.FAQs;

namespace Nop.Services.FAQs;

public interface IFaqProductService
{
    Task InsertFaqProductAsync(FaqProductMapping faqProduct);
}