using Nop.Core;
using Nop.Services.Plugins;

namespace Nop.Plugin.ProductProvider.Infigo;

public class InfigoProductProvider : BasePlugin
{
    protected readonly IWebHelper _webHelper;

    public InfigoProductProvider(IWebHelper webHelper)
    {
        _webHelper = webHelper;
    }

    public override string GetConfigurationPageUrl()
    {
        return $"{_webHelper.GetStoreLocation()}Admin/ProductProviderInfigo/Configure";
    }
}