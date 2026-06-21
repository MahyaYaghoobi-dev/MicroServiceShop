using Catalog.Core.Entities;
using Catalog.Core.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public class CatalogContext:ICatalogContext
{
    public IMongoCollection<Product> Products { get; }
    public IMongoCollection<ProductBrand> Brands { get; }
    public IMongoCollection<ProductType> Types { get; }

    public CatalogContext(IOptions<DatabaseSettings> options)
    {
        //get database settings 
        var settings = options.Value;
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);

        //get collections
        Products = database.GetCollection<Product>(settings.ProductsCollection);
        Brands=database.GetCollection<ProductBrand>(settings.BrandsCollection); 
        Types=database.GetCollection<ProductType>(settings.TypesCollection);
        
    }
}