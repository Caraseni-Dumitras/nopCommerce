using Nop.Core.Domain.Catalog;
using Nop.Data;
using Nop.Services.Catalog;
using Nop.Services.FAQs;
using Nop.Web.Models.FAQs;

namespace Nop.Web.Factories;

public class FaqModelFactory : IFaqModelFactory
{
    protected readonly ICategoryService      _categoryService;
    protected readonly IFaqService           _faqService;
    protected readonly IRepository<Category> _categoryRepository;

    public FaqModelFactory(ICategoryService categoryService, IFaqService faqService, IRepository<Category> repository)
    {
        _categoryService    = categoryService;
        _faqService         = faqService;
        _categoryRepository = repository;
    }

    public async Task<FaqIndexModel> PrepareFaqIndexModelAsync(int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);

        if (category == null)
        {
            var genericCategory = await _categoryRepository.Table.FirstOrDefaultAsync(c => c.Name == "Generic");
            categoryId = genericCategory.Id;
        }

        var faqIndexEntities = await _faqService.GetAllFaqByCategoryIdAsync(categoryId);
        var faqIndexModels   = new List<FaqModel>();

        foreach (var entity in faqIndexEntities)
        {
            var model = new FaqModel()
            {
                QuestionTitle = entity.QuestionTitle,
                QuestionDescription = entity.QuestionDescription,
                AnswerTitle = entity.AnswerTitle,
                AnswerDescription = entity.AnswerDescription,
                CategoryId = entity.CategoryId
            };
            faqIndexModels.Add(model);
        }

        return new FaqIndexModel() { FaqModels = faqIndexModels.OrderBy(f => f.QuestionTitle).ToList() };
    }
}