using System.Text.Json;
using Catalog.Core.Entities;
using MongoDB.Driver;

namespace Catalog.Infrastructure.Data;

public static class TypeSeeder
{
    public static void Seed(IMongoCollection<ProductType> collection)
    {
        var exists = collection.Find(_ => true).Any();
        if (exists) return;  
        
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "types.json");
        if (!File.Exists(jsonPath)) 
            throw new FileNotFoundException($"Type seed data file not found at: {jsonPath}");
        
        var dataText = File.ReadAllText(jsonPath);
        var types = JsonSerializer.Deserialize<List<ProductType>>(dataText);
        
        if (types != null && types.Any())
        {
            collection.InsertMany(types);
        }
    }
}