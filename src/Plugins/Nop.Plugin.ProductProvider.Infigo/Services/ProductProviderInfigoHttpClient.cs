using System.Net.Http;
using System.Threading.Tasks;

namespace Nop.Plugin.ProductProvider.Infigo.Services;

public class ProductProviderInfigoHttpClient
{
    private readonly HttpClient _httpClient;

    public ProductProviderInfigoHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task RequestAsync()
    {
    }
}