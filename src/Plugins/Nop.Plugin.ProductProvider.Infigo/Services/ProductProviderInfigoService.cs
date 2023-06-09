using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
    private readonly ProductProviderInfigoHttpClient _httpClient;
    private readonly ISettingService                 _settingService;
    private readonly IProductService                 _productService;
    private readonly IInfigoProductMapper            _infigoProductMapper;
    private readonly IProductAttributeService        _productAttributeService;
    private readonly IPictureService                 _pictureService;

    public ProductProviderInfigoService(ProductProviderInfigoHttpClient httpClient,              ISettingService      settingService,
                                        IProductService                 productService,          IInfigoProductMapper infigoProductMapper,
                                        IProductAttributeService        productAttributeService, IPictureService pictureService)
    {
        _httpClient              = httpClient;
        _settingService          = settingService;
        _productService          = productService;
        _infigoProductMapper     = infigoProductMapper;
        _productAttributeService = productAttributeService;
        _pictureService          = pictureService;
    }

    public async Task<List<int>> GetAllProducts()
    {
        var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        var url      = settings.BaseApiUrl + settings.GetAllProductsUrl;
        var apiType  = settings.ApiType;
        var apiKey   = settings.ApiKey;

        var data = await _httpClient.RequestAsync(url, apiType, apiKey);

        var productIdList = JsonConvert.DeserializeObject<List<int>>(data);

        return productIdList;
    }

    public async Task<ApiProductModel> GetProductById(int id)
    {
        var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        var url      = settings.BaseApiUrl + settings.GetProductByIdUrl + id;
        var apiType  = settings.ApiType;
        var apiKey   = settings.ApiKey;

        var data = await _httpClient.RequestAsync(url, apiType, apiKey);

        var product = JsonConvert.DeserializeObject<ApiProductModel>(data);

        return product;
    }

    public async Task Insert(ApiProductModel model)
    {
        var product    = _infigoProductMapper.ToEntity(model);
        await _productService.InsertProductAsync(product);

        await InsertProductAttributes(model.ProductAttributes, product);

        if (model.PreviewUrls != null)
        {
            await InsertProductPicture(model, product);   
        }
    }

    private async Task InsertProductAttributes(ICollection<ApiProductAttributeModel> attributes, Product product)
    {
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
        foreach (var productAttributeValueModel in models)
        {
            var productAttributeValue = _infigoProductMapper.ToEntity(productAttributeValueModel, attributeMapping, product);
            await _productAttributeService.InsertProductAttributeValueAsync(productAttributeValue);
        }
    }

    private async Task InsertProductPicture(ApiProductModel model, Product product)
    { 
        var settings = await _settingService.LoadSettingAsync<ProductProviderInfigoSettings>();
        var apiType  = settings.ApiType;
        var apiKey   = settings.ApiKey;

        foreach (var url in model.ThumbnailUrls)
        {
            var imageBinary = await _httpClient.RequestPictureAsync(url, apiType, apiKey);

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