using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.ProductProvider.Infigo.Mappers;
using Nop.Plugin.ProductProvider.Infigo.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Media;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoService : IProductProviderInfigoService
{
    private readonly ProductProviderInfigoHttpClient       _httpClient;
    private readonly IProductService                       _productService;
    private readonly IInfigoProductMapper                  _infigoProductMapper;
    private readonly IProductAttributeService              _productAttributeService;
    private readonly IPictureService                       _pictureService;
    private readonly ILogger<ProductProviderInfigoService> _logger;
    private readonly ISettingService                       _settingService;
    private readonly ISpecificationAttributeService        _specificationAttributeService;

    public ProductProviderInfigoService(ProductProviderInfigoHttpClient       httpClient, 
                                        IProductService                       productService, 
                                        IInfigoProductMapper                  infigoProductMapper,
                                        IProductAttributeService              productAttributeService, 
                                        IPictureService                       pictureService, 
                                        ILogger<ProductProviderInfigoService> logger, 
                                        ISettingService                       settingService, ISpecificationAttributeService specificationAttributeService)
    {
        _httpClient                         = httpClient;
        _productService                     = productService;
        _infigoProductMapper                = infigoProductMapper;
        _productAttributeService            = productAttributeService;
        _pictureService                     = pictureService;
        _logger                             = logger;
        _settingService                     = settingService;
        _specificationAttributeService = specificationAttributeService;
    }

    public async Task Insert(ApiProductModel model)
    {
        _logger.LogDebug("Insert all synchronized products");
        var product = _infigoProductMapper.ToEntity(model);
        
        await _productService.InsertProductAsync(product);

        await InsertProductAttributes(model.ProductAttributes, product);
        
        var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        
        if (!model.ThumbnailUrls.Any())
        {
            if (String.IsNullOrEmpty(settings.DefaultProductPictureUrl))
            {
                var tempProduct = await _httpClient.RequestProductByIdAsync(1548);
                settings.DefaultProductPictureUrl = tempProduct.ThumbnailUrls.FirstOrDefault();
            }
            model.ThumbnailUrls.Add(settings.DefaultProductPictureUrl);
        }
        
        await InsertProductPicture(model, product);

        await InsertSpecificationAttributeOption(model.Id);
    }

    private async Task InsertProductAttributes(ICollection<ApiProductAttributeModel> attributes, Product product)
    {
        _logger.LogDebug("Insert all product attributes");
        foreach (var productAttributeModel in attributes)
        {
            var productAttribute = _infigoProductMapper.ToEntity(productAttributeModel);
            await _productAttributeService.InsertProductAttributeAsync(productAttribute);
            
            var productAttributeMapping = _infigoProductMapper.ToEntity(productAttribute, product, productAttributeModel);
            await _productAttributeService.InsertProductAttributeMappingAsync(productAttributeMapping);

            await InsertProductAttributeValue(productAttributeModel.ProductAttributeValues, productAttributeMapping,
                product);
        }
    }

    private async Task InsertProductAttributeValue(ICollection<ApiProductAttributeValueModel> models,
                                                   ProductAttributeMapping                    attributeMapping,
                                                   Product                                    product)
    {
        _logger.LogDebug("Insert all attribute values");
        foreach (var productAttributeValueModel in models)
        {
            var productAttributeValue = _infigoProductMapper.ToEntity(productAttributeValueModel, attributeMapping, product);
            await _productAttributeService.InsertProductAttributeValueAsync(productAttributeValue);
        }
    }

    private async Task InsertProductPicture(ApiProductModel model, Product product)
    {
        _logger.LogDebug("Insert product pictures");
        foreach (var url in model.ThumbnailUrls)
        {
            var imageBinary = await _httpClient.RequestPicturesAsync(url);
            var fileInfo    = new FileInfo(url);
            var mimeType    = "image/" + fileInfo.Extension.Remove(0, 1);
            var picture     = await _pictureService.InsertPictureAsync(imageBinary, mimeType, model.Name);
            var productPicture = new ProductPicture
            {
                PictureId = picture.Id, 
                ProductId = product.Id
            }; 
            await _productService.InsertProductPictureAsync(productPicture);   
        }
    }

    private async Task InsertSpecificationAttributeOption(int productId)
    {
        var specificationAttribute   = await _specificationAttributeService.GetSpecificationAttributesAsync();
        var specificationAttributeId = specificationAttribute.Where(s => s.Name == "ExternalId").ToString();
        var specificationAttributeOption = _infigoProductMapper.ToEntity(int.Parse(specificationAttributeId), productId.ToString());
        
        await _specificationAttributeService.InsertSpecificationAttributeOptionAsync(specificationAttributeOption);
        
        await InsertProductSpecificationAttributeMapping(specificationAttributeOption.Id, productId);
    }

    private async Task InsertProductSpecificationAttributeMapping(int specificationAttributeOptionId, int productId)
    {
        var productSpecificationAttribute = _infigoProductMapper.ToEntity(specificationAttributeOptionId, productId);

        await _specificationAttributeService.InsertProductSpecificationAttributeAsync(productSpecificationAttribute);
    }
}