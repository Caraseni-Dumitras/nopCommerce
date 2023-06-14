using Nop.Web.Framework.Models;

namespace Nop.Web.Models.FAQs;

public record FaqIndexModel : BaseNopModel
{
    public List<FaqModel> FaqModels { get; set; } = new();
}