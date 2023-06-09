using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

    public ProductProviderInfigoService(ProductProviderInfigoHttpClient       httpClient, 
                                        IProductService                       productService, 
                                        IInfigoProductMapper                  infigoProductMapper,
                                        IProductAttributeService              productAttributeService, 
                                        IPictureService                       pictureService, 
                                        ILogger<ProductProviderInfigoService> logger, 
                                        ISettingService                       settingService)
    {
        _httpClient              = httpClient;
        _productService          = productService;
        _infigoProductMapper     = infigoProductMapper;
        _productAttributeService = productAttributeService;
        _pictureService          = pictureService;
        _logger                  = logger;
        _settingService     = settingService;
    }

    public async Task<List<int>> GetAllProductsIds()
    {
        _logger.LogDebug("Getting all products ids");
        var data = await _httpClient.RequestAllProductsIdsAsync();

        var productIdList = JsonConvert.DeserializeObject<List<int>>(data);

        return productIdList;
    }

    public async Task<ApiProductModel> GetProductById(int id)
    {
        _logger.LogDebug("Getting product by id {Id}", id);
        var data = await _httpClient.RequestProductByIdAsync(id);

        var product = JsonConvert.DeserializeObject<ApiProductModel>(data);

        return product;
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
            if (!String.IsNullOrEmpty(settings.DefaultProductPictureUrl))
            {
                model.ThumbnailUrls.Add(settings.DefaultProductPictureUrl);
            }
        }

        if (model.ThumbnailUrls.Any())
        {
            await InsertProductPicture(model, product);
        }
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

            var fileInfo = new FileInfo(url);

            var mimeType = "image/" + fileInfo.Extension.Remove(0,1);
            
            var picture = await _pictureService.InsertPictureAsync(imageBinary, mimeType, model.Name);
            var productPicture = new ProductPicture
            {
                PictureId = picture.Id, 
                ProductId = product.Id
            }; 
            await _productService.InsertProductPictureAsync(productPicture);   
        }
    }
}