using Nop.Core.Domain.Catalog;
using Nop.Plugin.ProductProvider.Infigo.Models;

namespace Nop.Plugin.ProductProvider.Infigo.Mappers;

public interface IInfigoProductMapper
{
    public Product          ToEntity(ApiProductModel          model);
    public ProductAttribute ToEntity(ApiProductAttributeModel model);
    public ProductAttributeValue ToEntity(ApiProductAttributeValueModel model,
                                          ProductAttributeMapping       productAttributeMapping, 
                                          Product                       product);
    public ProductAttributeMapping ToEntity(ProductAttribute         productAttribute,
                                            Product                  product, 
                                            ApiProductAttributeModel model);
}