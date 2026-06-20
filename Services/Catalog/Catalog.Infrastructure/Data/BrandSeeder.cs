using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class BrandSeeder
{
    public static void Seed(IMongoCollection<ProductBrand> collection)
    {
        var exists = collection.Find(_ => true).Any();
        if (exists) return;  
        
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "brands.json");
        if (!File.Exists(jsonPath)) 
            throw new FileNotFoundException($"Brand seed data file not found at: {jsonPath}");
        
        var dataText = File.ReadAllText(jsonPath);
        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(dataText);
        
        if (brands != null && brands.Any())
        {
            collection.InsertMany(brands);
        }
    }
    
}