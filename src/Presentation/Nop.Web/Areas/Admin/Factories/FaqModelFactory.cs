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
    protected readonly IFaqProductService     _faqProductService;

    public FaqModelFactory(IFaqService faqService, ICategoryService categoryService, IBaseAdminModelFactory baseAdminModelFactory, IFaqProductService faqProductService)
    {
        _faqService             = faqService;
        _categoryService        = categoryService;
        _baseAdminModelFactory  = baseAdminModelFactory;
        _faqProductService = faqProductService;
    }

    public async Task<FaqSearchModel> PrepareFaqSearchModelAsync(FaqSearchModel searchModel)
    {
        if (searchModel == null)
            throw new ArgumentNullException(nameof(searchModel));
        
        await _baseAdminModelFactory.PrepareCategoriesAsync(searchModel.AvailableCategories);
    
        searchModel.SetGridPageSize();
    
        return searchModel;
    }
    
    public async Task<FaqListModel> PrepareFaqListModelAsync(FaqSearchModel searchModel)
    {
        if (searchModel == null)
            throw new ArgumentNullException(nameof(searchModel));
    
        var categoryIds = new List<int>();
        if (searchModel.SearchCategoryId > 0)
        {
            categoryIds.Add(searchModel.SearchCategoryId);
            if (searchModel.SearchIncludeSubCategories)
            {
                var childCategoryIds = await _categoryService.GetChildCategoryIdsAsync(parentCategoryId: searchModel.SearchCategoryId, showHidden: true);
                categoryIds.AddRange(childCategoryIds);
            }
        }
    
        var faqs = await _faqService.GetAllFaqsAsync(categoryIds);
        
        var pagedFaqs = faqs.ToPagedList(searchModel);
    
        var model = await new FaqListModel().PrepareToGridAsync(searchModel, pagedFaqs, () =>
        {
            return pagedFaqs.SelectAwait(async faq =>
            {
                var categories = await _faqService.GetFaqCategoriesAsync(faq.Id);
                var products  = await _faqService.GetFaqProductsAsync(faq.Id);
                var faqModel =  new FaqModel()
                {
                    Id                  = faq.Id,
                    QuestionTitle       = faq.QuestionTitle,
                    QuestionDescription = faq.QuestionDescription,
                    AnswerTitle         = faq.AnswerTitle,
                    AnswerDescription   = faq.AnswerDescription,
                    CategoryIds         = categories.Select(it => it.Id).ToList(),
                    CategoryName        = categories.Select(it => it.Name).ToList(),
                    CreatedOnUtc        = faq.CreatedOnUtc,
                    UpdatedOnUtc        = faq.UpdatedOnUtc,
                    ProductName         = products.Select(it => it.Name).ToList()
                };
    
                return faqModel;
            });
        });
    
        return model;
    }
    
    public async Task<FaqModel> PrepareFaqCategoryModelAsync(FaqModel model, Faq faq)
    {
        var categories = await _categoryService.GetAllCategoriesAsync(showHidden:true);
        if (faq != null)
        {
            var categoriesIds = new List<int>();
            foreach (var item in categories)
            {
                categoriesIds.Add(item.Id);
            }
        
            var faqCategories = await _faqService.GetFaqCategoriesAsync(faq.Id);
        
            model = new FaqModel()
            {
                Id                  = faq.Id,
                QuestionTitle       = faq.QuestionTitle,
                QuestionDescription = faq.QuestionDescription,
                AnswerTitle         = faq.AnswerTitle,
                AnswerDescription   = faq.AnswerDescription,
                CategoryIds         = faqCategories.Select(it => it.Id).ToList(),
                CategoryName        = faqCategories.Select(it => it.Name).ToList(),
                SelectedCategoryIds = categoriesIds
            };
        }
    
        if (faq == null)
        {
            var genericCategory = categories.Where(c => c.Name == "Generic").ToList();
            model.CategoryIds  = genericCategory.Select(it => it.Id).ToList();
            model.CategoryName = genericCategory.Select(it => it.Name).ToList();
        }
    
        await _baseAdminModelFactory.PrepareCategoriesAsync(model.AvailableCategories, false);
        foreach (var categoryItem in model.AvailableCategories)
        {
            categoryItem.Selected = int.TryParse(categoryItem.Value, out var categoryId)
                                    && model.SelectedCategoryIds.Contains(categoryId);
        }  
        
        return model;
    }

    public async Task<FaqModel> PrepareFaqProductModelAsync(FaqModel model, Faq faq)
    {
        if (faq != null)
        {
            var faqProducts = await _faqService.GetFaqProductsAsync(faq.Id);
        
            model = new FaqModel()
            {
                Id                  = faq.Id,
                QuestionTitle       = faq.QuestionTitle,
                QuestionDescription = faq.QuestionDescription,
                AnswerTitle         = faq.AnswerTitle,
                AnswerDescription   = faq.AnswerDescription,
                ProductIds = faqProducts.Select(it => it.Id).ToList()
            };
        }  
        
        return model;
    }

    // public async Task<FaqListModel> PrepareFaqProductListModelAsync(FaqSearchModel searchModel)
    // {
    //     if (searchModel == null)
    //         throw new ArgumentNullException(nameof(searchModel));
    //
    //     var faqProducts = await _faqProductService.GetAllFaqByProductsIdAsync(searchModel.SearchProductId);
    //     var faqsIds     = new List<int>();
    //
    //     foreach (var item in faqProducts)
    //     {
    //         faqsIds.Add(item.FaqId);
    //     }
    //
    //     var faqs = await _faqService.GetAllFaqByIdsAsync(faqsIds);
    //     
    //     var pagedFaqs = faqs.ToPagedList(searchModel);
    //
    //     var model = await new FaqListModel().PrepareToGridAsync(searchModel, pagedFaqs, () =>
    //     {
    //         return pagedFaqs.SelectAwait(async faq =>
    //         {
    //             var category = await _categoryService.GetCategoryByIdAsync(faq.CategoryId);
    //             var faqModel =  new FaqModel()
    //             {
    //                 Id                  = faq.Id,
    //                 QuestionTitle       = faq.QuestionTitle,
    //                 QuestionDescription = faq.QuestionDescription,
    //                 AnswerTitle         = faq.AnswerTitle,
    //                 AnswerDescription   = faq.AnswerDescription,
    //                 CategoryId          = faq.CategoryId,
    //                 CategoryName        = category.Name
    //             };
    //
    //             return faqModel;
    //         });
    //     });
    //
    //     return model;
    // }
}