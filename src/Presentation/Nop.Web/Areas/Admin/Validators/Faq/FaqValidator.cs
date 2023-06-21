using FluentValidation;
using Nop.Data.Mapping;
using Nop.Services.Localization;
using Nop.Web.Areas.Admin.Models.FAQs;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Areas.Admin.Validators.Faq;

public class FaqValidator : BaseNopValidator<FaqModel>
{
    public FaqValidator(ILocalizationService localizationService, IMappingEntityAccessor mappingEntityAccessor)
    {
        RuleFor(f => f.QuestionTitle)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Admin.ContentManagement.FAQ.QuestionTitle.Required"));

        RuleFor(f => f.QuestionDescription)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Admin.ContentManagement.FAQ.QuestionDescription.Required"));
        
        RuleFor(f => f.AnswerTitle)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Admin.ContentManagement.FAQ.AnswerTitle.Required"));

        RuleFor(f => f.AnswerDescription)
            .NotEmpty()
            .WithMessageAwait(localizationService.GetResourceAsync("Admin.ContentManagement.FAQ.AnswerDescription.Required"));
        
        SetDatabaseValidationRules<Core.Domain.FAQs.Faq>(mappingEntityAccessor);
    }   
}