using Nop.Core.Configuration;

namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigoSettings : ISettings
{
    public string ExternalApiUrl    { get; set; }
}