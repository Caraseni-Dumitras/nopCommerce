using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.ProductProvider.Infigo.Models;

public record ConfigurationModel
{
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.ExternalApiUrl")]
    public string ExternalApiUrl { get; set; }
}