using Nop.Web.Framework.Models;

namespace Nop.Plugin.ProductProvider.Infigo.Models;

public record ConfigurationModel : BaseNopModel
{
    public int    Id   { get; set; }
    public string Name { get; set; }
}