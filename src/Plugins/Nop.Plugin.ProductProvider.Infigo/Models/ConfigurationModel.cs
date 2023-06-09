using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.ProductProvider.Infigo.Models;

public record ConfigurationModel
{
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.BaseApiUrl")]
    public string BaseApiUrl        { get; set; }
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.ApiKey")]
    public string ApiKey            { get; set; }
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.GetAllProductsUrl")]
    public string GetAllProductsUrl { get; set; }
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.GetProductByIdUrl")]
    public string GetProductByIdUrl { get; set; }
    [NopResourceDisplayName("Plugins.ProductProvider.Infigo.DefaultProductPictureUrl")]
    public string DefaultProductPictureUrl { get; set; }
}