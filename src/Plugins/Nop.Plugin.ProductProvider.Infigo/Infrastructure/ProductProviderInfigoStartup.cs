using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.ProductProvider.Infigo.Mappers;
using Nop.Plugin.ProductProvider.Infigo.Services;

namespace Nop.Plugin.ProductProvider.Infigo.Infrastructure;

public class ProductProviderInfigoStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ProductProviderInfigoHttpClient>();

        services.AddScoped<IProductProviderInfigoService, ProductProviderInfigoService>();
        
        services.AddScoped<IInfigoProductMapper, InfigoProductMapper>();
    }
        
    public void Configure(IApplicationBuilder application)
    {
    }
    
    public int Order => 3000;
    }
