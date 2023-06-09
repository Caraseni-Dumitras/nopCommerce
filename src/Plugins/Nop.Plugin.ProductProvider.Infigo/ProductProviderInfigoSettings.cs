using Nop.Core.Configuration;

namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigoSettings : ISettings
{
    public string BaseApiUrl            { get; set; }
    public string ApiKey                { get; set; }
    public string GetAllProductsUrl     { get; set; }
    public string GetProductByIdUrl     { get; set; }
    public string DefaultProductPicture { get; set; }
}