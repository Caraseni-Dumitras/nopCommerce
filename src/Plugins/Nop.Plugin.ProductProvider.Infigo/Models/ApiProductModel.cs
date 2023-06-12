using System.Collections.Generic;

namespace Nop.Plugin.ProductProvider.Infigo.Models;

public class ApiProductModel
{
    public int                                   Id                      { get; set; }
    public string                                Name                    { get; set; }
    public string                                ShortDescription        { get; set; }
    public string                                LongDescription         { get; set; }
    public int                                   Type                    { get; set; }
    public decimal                               Price                   { get; set; }
    public int                                   StockValue              { get; set; }
    public string                                Sku                     { get; set; }
    public List<string>                          PreviewUrls             { get; set; } = new();
    public List<string>                          ThumbnailUrls           { get; set; } = new();
    public List<string>                          Tags                    { get; set; } = new();
    public List<ApiProductAttributeModel>        ProductAttributes       { get; set; } = new();
    public List<ApiAttributeCombinationsModel>   AttributeCombinations   { get; set; } = new();
    public List<ApiMisConfigurationsModel>       MisConfigurations       { get; set; } = new();
    public List<ApiSpecificationAttributesModel> SpecificationAttributes { get; set; } = new();
    
}