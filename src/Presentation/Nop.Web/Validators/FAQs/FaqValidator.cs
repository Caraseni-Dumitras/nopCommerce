using FluentValidation;
using Nop.Data.Mapping;
using Nop.Services.Localization;
using Nop.Web.Models.FAQs;
using Nop.Web.Framework.Validators;

namespace Nop.Web.Validators.FAQs;

public class FaqValidator : BaseNopValidator<FaqModel>
{
    public FaqValidator(ILocalizationService localizationService, IMappingEntityAccessor mappingEntityAccessor)
    {
        RuleFor(f => f.QuestionTitle)
            .NotEmpty();
        
        RuleFor(f => f.QuestionDescription)
            .NotEmpty();
        
        RuleFor(f => f.AnswerTitle)
            .NotEmpty();
        
        RuleFor(f => f.AnswerDescription)
            .NotEmpty();
    }
}