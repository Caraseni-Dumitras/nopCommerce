using Nop.Core.Domain.FAQs;
using Nop.Web.Areas.Admin.Models.FAQs;

namespace Nop.Web.Areas.Admin.Factories;

public interface IFaqModelFactory
{
    Task<FaqSearchModel> PrepareFaqSearchModelAsync(FaqSearchModel searchModel);
    Task<FaqListModel>   PrepareFaqListModelAsync(FaqSearchModel   searchModel);
    Task<FaqModel>               PrepareFaqModelAsync(Faq faq);
}