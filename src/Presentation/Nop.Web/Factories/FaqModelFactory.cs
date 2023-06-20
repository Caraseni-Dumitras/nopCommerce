using LinqToDB.Common;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.FAQs;
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
    protected readonly IFaqProductService    _faqProductService;

    public FaqModelFactory(ICategoryService categoryService, IFaqService faqService, IRepository<Category> repository, IFaqProductService faqProductService)
    {
        _categoryService    = categoryService;
        _faqService         = faqService;
        _categoryRepository = repository;
        _faqProductService  = faqProductService;
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

        var faqProduct = await _faqProductService.GetAllFaqWithoutProductAsync();
        var faqIds     = new List<int>();
        foreach (var item in faqProduct)
        {
            faqIds.Add(item.FaqId);
        }

        var faqIndexEntitiesResponse = new List<Faq>();
        foreach (var item in faqIndexEntities)
        {
            if (faqIds.Contains(item.Id))
            {
                faqIndexEntitiesResponse.Add(item);
            }
        }
        
        var faqIndexModels   = new List<FaqModel>();

        foreach (var entity in faqIndexEntitiesResponse)
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

    public async Task<FaqIndexModel> PrepareFaqProductIndexModelAsync(int productId)
    {
        var faqProducts = await _faqProductService.GetAllFaqByProductsIdAsync(productId);

        if (faqProducts.IsNullOrEmpty())
        {
            return new FaqIndexModel();
        }

        var faqIndexIds = new List<int>();

        foreach (var item in faqProducts)
        {
            faqIndexIds.Add(item.FaqId);
        }
        
        var faqIndexEntities = await _faqService.GetAllFaqByIdsAsync(faqIndexIds);
        var faqIndexModels   = new List<FaqModel>();

        foreach (var entity in faqIndexEntities)
        {
            var model = new FaqModel()
            {
                QuestionTitle       = entity.QuestionTitle,
                QuestionDescription = entity.QuestionDescription,
                AnswerTitle         = entity.AnswerTitle,
                AnswerDescription   = entity.AnswerDescription,
                CategoryId          = entity.CategoryId
            };
            faqIndexModels.Add(model);
        }

        return new FaqIndexModel() { FaqModels = faqIndexModels.OrderBy(f => f.QuestionTitle).ToList() };
    }
}