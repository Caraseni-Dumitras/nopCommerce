using System.Collections.Generic;

namespace Nop.Plugin.ProductProvider.Infigo.Models;

public class ApiProductAttributeModel
{
    public string                              Name                   { get; set; }
    public string                              Description            { get; set; }
    public int                                 AttributeControlType   { get; set; }
    public bool                                IsRequired             { get; set; }
    public List<ApiProductAttributeValueModel> ProductAttributeValues { get; set; } = new();
    public List<ApiMisConfigurationsModel>     MisConfigurations      { get; set; } = new();
}