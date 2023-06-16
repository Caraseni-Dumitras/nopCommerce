using Nop.Core.Domain.FAQs;
using Nop.Services.Catalog;
using Nop.Services.FAQs;
using Nop.Web.Areas.Admin.Models.FAQs;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Web.Areas.Admin.Factories;

public class FaqModelFactory : IFaqModelFactory
{
    protected readonly IFaqService            _faqService;
    protected readonly ICategoryService       _categoryService;
    protected readonly IBaseAdminModelFactory _baseAdminModelFactory;

    public FaqModelFactory(IFaqService faqService, ICategoryService categoryService, IBaseAdminModelFactory baseAdminModelFactory)
    {
        _faqService                 = faqService;
        _categoryService            = categoryService;
        _baseAdminModelFactory = baseAdminModelFactory;
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

    public async Task<FaqModel> PrepareFaqModelAsync(FaqModel model, Faq faq)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(showHidden:true);
        if (faq != null)
        {
            var categoriesIds = new List<int>();
            foreach (var item in categories)
            {
                categoriesIds.Add(item.Id);
            }
        
            var category = await _categoryService.GetCategoryByIdAsync(faq.CategoryId);
        
            model = new FaqModel()
            {
                Id                  = faq.Id,
                QuestionTitle       = faq.QuestionTitle,
                QuestionDescription = faq.QuestionDescription,
                AnswerTitle         = faq.AnswerTitle,
                AnswerDescription   = faq.AnswerDescription,
                CategoryId          = faq.CategoryId,
                CategoryName        = category.Name,
                SelectedCategoryIds = categoriesIds
            };
        }

        if (faq == null)
        {
            var genericCategory = categories.FirstOrDefault(c => c.Name == "Generic");
            model.CategoryId   = genericCategory.Id;
            model.CategoryName = genericCategory.Name;
        }

        await _baseAdminModelFactory.PrepareCategoriesAsync(model.AvailableCategories, false);
        foreach (var categoryItem in model.AvailableCategories)
        {
            categoryItem.Selected = int.TryParse(categoryItem.Value, out var categoryId)
                                    && model.SelectedCategoryIds.Contains(categoryId);
        }  
        
        return model;
    }
}