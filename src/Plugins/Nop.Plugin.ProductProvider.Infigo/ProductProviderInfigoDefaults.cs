namespace Nop.Plugin.ProductProvider.Infigo;

public class ProductProviderInfigoDefaults
{
    public static string SystemName             => "ProductProvider.Infigo";
    public static string ConfigurationRouteName => "Plugin.ProductProvider.Infigo.Configure";
    public static (string Name, string Type) SyncProductsTask =>
        ($"Sync products ({SystemName})", "Nop.Plugin.ProductProvider.Infigo.BackgroundTasks.SyncProductsTask");
}