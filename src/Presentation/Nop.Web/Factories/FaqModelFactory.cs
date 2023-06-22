using Nop.Services.Catalog;
using Nop.Services.FAQs;
using Nop.Web.Models.FAQs;

namespace Nop.Web.Factories;

public class FaqModelFactory : IFaqModelFactory
{
    protected readonly ICategoryService      _categoryService;
    protected readonly IFaqService           _faqService;

    public FaqModelFactory(ICategoryService categoryService, IFaqService faqService)
    {
        _categoryService    = categoryService;
        _faqService         = faqService;
    }

    public async Task<FaqIndexModel> PrepareFaqIndexModelAsync(int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);

        if (category == null)
        {
            var genericCategory = await _categoryService.GetCategoryByNameAsync("Generic");
            categoryId = genericCategory.Id;
        }

        var faqEntities    = await _faqService.GetAllFaqByCategoryIdAsync(categoryId);
        var faqIndexModels = new List<FaqModel>();

        foreach (var entity in faqEntities)
        {
            var model = new FaqModel()
            {
                QuestionTitle = entity.QuestionTitle,
                QuestionDescription = entity.QuestionDescription,
                AnswerTitle = entity.AnswerTitle,
                AnswerDescription = entity.AnswerDescription,
            };
            faqIndexModels.Add(model);
        }
        return new FaqIndexModel() { FaqModels = faqIndexModels.OrderBy(f => f.QuestionTitle).ToList() };
    }

    public async Task<FaqIndexModel> PrepareFaqProductIndexModelAsync(int productId)
    {
        var faqEntities    = await _faqService.GetAllFaqByProductsIdAsync(productId);
        var faqIndexModels = new List<FaqModel>();
    
        foreach (var entity in faqEntities)
        {
            var model = new FaqModel()
            {
                QuestionTitle       = entity.QuestionTitle,
                QuestionDescription = entity.QuestionDescription,
                AnswerTitle         = entity.AnswerTitle,
                AnswerDescription   = entity.AnswerDescription
            };
            faqIndexModels.Add(model);
        }
        return new FaqIndexModel() { FaqModels = faqIndexModels.OrderBy(f => f.QuestionTitle).ToList() };
    }
}