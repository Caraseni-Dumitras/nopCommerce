using System;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.ProductProvider.Infigo.Models;

namespace Nop.Plugin.ProductProvider.Infigo.Mappers;

public class InfigoProductMapper : IInfigoProductMapper
{
    public Product ToEntity(ApiProductModel model)
    {
        var insertTime = DateTime.UtcNow;
        
        return new Product
        {
            Name             = model.Name,
            ShortDescription = model.ShortDescription,
            FullDescription  = model.LongDescription,
            ProductTypeId    = model.Type,
            Price            = model.Price,
            StockQuantity    = model.StockValue,
            Sku              = model.Sku,
            CreatedOnUtc     = insertTime,
            UpdatedOnUtc     = insertTime
        };
    }

    public ProductAttribute ToEntity(ApiProductAttributeModel model)
    {
        return new ProductAttribute
        {
            Name        = model.Name, 
            Description = model.Description,
        };
    }

    public ProductAttributeValue ToEntity(ApiProductAttributeValueModel model,
                                          ProductAttributeMapping       productAttributeMapping, Product product)
    {
        return new ProductAttributeValue
        {
            Name                      = model.Name,
            ProductAttributeMappingId = productAttributeMapping.Id,
            AssociatedProductId       = product.Id,
            PriceAdjustment           = model.PriceAdjustment,
            WeightAdjustment          = model.WeightAdjustment
        };
    }

    public ProductAttributeMapping ToEntity(ProductAttribute productAttribute,
                                            Product          product, ApiProductAttributeModel model)
    {
        return new ProductAttributeMapping
        {
            ProductAttributeId     = productAttribute.Id,
            ProductId              = product.Id,
            IsRequired             = model.IsRequired,
            AttributeControlTypeId = model.AttributeControlType
        };
    }

    public SpecificationAttributeOption ToEntity(int specificationAttributeId, string productId)
    {
        return new SpecificationAttributeOption()
        {
            Name = productId, SpecificationAttributeId = specificationAttributeId
        };
    }

    public ProductSpecificationAttribute ToEntity(int specificationAttributeOptionId, int productId)
    {
        return new ProductSpecificationAttribute()
        {
            ProductId = productId,
            SpecificationAttributeOptionId = specificationAttributeOptionId
        };
    }
}