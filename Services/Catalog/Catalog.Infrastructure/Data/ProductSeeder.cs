using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class ProductSeeder
{
    public static void Seed(IMongoCollection<Product> collection)
    {
        var exists = collection.Find(_ => true).Any();
        if (exists) return;  
        
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "products.json");
        if (!File.Exists(jsonPath)) 
            throw new FileNotFoundException($"Product seed data file not found at: {jsonPath}");
        
        var dataText = File.ReadAllText(jsonPath);
        var products = JsonSerializer.Deserialize<List<Product>>(dataText);
        
        if (products != null && products.Any())
        {
            collection.InsertMany(products);
        }
    }
}