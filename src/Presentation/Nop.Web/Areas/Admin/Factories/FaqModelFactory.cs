using Nop.Core.Domain.FAQs;
using Nop.Services.Catalog;
using Nop.Services.FAQs;
using Nop.Web.Areas.Admin.Models.FAQs;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

public class FaqModelFactory : IFaqModelFactory
{
    protected readonly IFaqService      _faqService;
    protected readonly ICategoryService _categoryService;

    public FaqModelFactory(IFaqService faqService, ICategoryService categoryService)
    {
        _faqService           = faqService;
        _categoryService = categoryService;
    }

    public async Task<FaqSearchModel> PrepareFaqSearchModelAsync(FaqSearchModel searchModel)
    {
        if (searchModel == null)
            throw new ArgumentNullException(nameof(searchModel));

        searchModel.SetGridPageSize();

        return searchModel;
    }

    public async Task<FaqListModel> PrepareFaqListModelAsync(FaqSearchModel searchModel)
    {
        if (searchModel == null)
            throw new ArgumentNullException(nameof(searchModel));

        var faqs = await _faqService.GetAllFaqsAsync();
        var pagedFaqs = faqs.ToPagedList(searchModel);

        var model = await new FaqListModel().PrepareToGridAsync(searchModel, pagedFaqs, () =>
        {
            return pagedFaqs.SelectAwait(async faq =>
            {
                var category = await _categoryService.GetCategoryByIdAsync(faq.CategoryId);
                var faqModel =  new FaqModel()
                {
                    Id = faq.Id,
                    QuestionTitle =faq.QuestionTitle,
                    QuestionDescription = faq.QuestionDescription,
                    AnswerTitle = faq.AnswerTitle,
                    AnswerDescription = faq.AnswerDescription,
                    CategoryId = faq.CategoryId,
                    CategoryName = category.Name
                };

                return faqModel;
            });
        });

        return model;
    }

    public async Task<FaqModel> PrepareFaqModelAsync(Faq faq)
    {
        var category = await _categoryService.GetCategoryByIdAsync(faq.CategoryId);
        
        return new FaqModel()
        {
            Id                  = faq.Id,
            QuestionTitle       = faq.QuestionTitle,
            QuestionDescription = faq.QuestionDescription,
            AnswerTitle         = faq.AnswerTitle,
            AnswerDescription   = faq.AnswerDescription,
            CategoryId          = faq.CategoryId,
            CategoryName        = category.Name
        };
    }
}