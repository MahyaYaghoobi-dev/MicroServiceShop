using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class CatalogContextSeed
{
    public static void SeedData(IMongoCollection<Product> products,
        IMongoCollection<ProductBrand> brands,
        IMongoCollection<ProductType> types)
    {
        // Seed Brands
        BrandSeeder.Seed(brands);
        
        // Seed Types
        TypeSeeder.Seed(types);
        
        // Seed Products
        ProductSeeder.Seed(products);
    }
}