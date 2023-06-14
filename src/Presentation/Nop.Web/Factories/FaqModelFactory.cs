using Nop.Services.Catalog;
using Nop.Web.Models.FAQs;

namespace Nop.Web.Factories;

public class FaqModelFactory : IFaqModelFactory
{
    protected readonly ICategoryService _categoryService;

    public FaqModelFactory(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<FaqIndexModel> PrepareFaqIndexModelAsync(int categoryId)
    {
        var category = await _categoryService.GetCategoryByIdAsync(categoryId);

        if (category == null)
        {
            var categoryList    = await _categoryService.GetAllCategoriesAsync(categoryName:"Generic", showHidden:true);
            var genericCategory = categoryList.FirstOrDefault(c => c.Name == "Generic");
            categoryId = genericCategory.Id;
        }
        
        var faqModels = new List<FaqModel>();
        faqModels.Add(new FaqModel()
        {
            QuestionTitle       = "Why FAQ Pages Are A Priority",
            QuestionDescription = "QD",
            AnswerTitle         = "AT",
            AnswerDescription   = "Your FAQ section should be seen as a constantly expanding source of value provided to your audience. It is a place where their ever-changing and growing requirements are not only met but anticipated and exceeded frequently.",
            CategoryId          = 1
        });
        faqModels.Add(new FaqModel()
        {
            QuestionTitle       = "Why An FAQ Resource?",
            QuestionDescription = "QD",
            AnswerTitle         = "AT",
            AnswerDescription   = "Firstly, FAQ pages can bring new visitors to your website via organic search and drive them quickly to related pages – most typically deeper blog pages and service pages closely related to the questions being resolved.",
            CategoryId          = 17
        });
        faqModels.Add(new FaqModel()
        {
            QuestionTitle       = "Use service data to identify your most common questions.",
            QuestionDescription = "QD",
            AnswerTitle         = "AT",
            AnswerDescription   = "Your FAQ page should address the most common questions customers have about your products, services, and brand as a whole. The best way to identify those questions is to tap into your customer service data and see which problems customers are consistently reaching out to you with.",
            CategoryId          = 17
        });
        faqModels.Add(new FaqModel()
        {
            QuestionTitle       = "Decide how you'll organize the FAQ page",
            QuestionDescription = "QD",
            AnswerTitle         = "AT",
            AnswerDescription   = "As you'll see from the examples below, not every FAQ page looks the same. Depending on what your company is selling and how many products it offers, your FAQ page might consist of a single page with a list of questions or several pages linked together. What's best for your business will vary based on the needs of your customers and how easy it is to troubleshoot your products.",
            CategoryId          = 17
        });
        
        //will be change when add services and will extract data from data base

        return new FaqIndexModel()
        {
            FaqModels = faqModels.Where(f => f.CategoryId == categoryId).ToList()
        };
    }
}