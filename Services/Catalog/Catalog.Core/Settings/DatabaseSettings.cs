namespace Catalog.Core.Settings;

public class DatabaseSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string ProductsCollection { get; set; }
    public required string BrandsCollection { get; set; }
    public required string TypesCollection { get; set; }
}