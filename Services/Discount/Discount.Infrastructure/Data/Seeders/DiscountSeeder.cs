using System.Text.Json;
using Dapper;
using Discount.Core.Entities;
using Discount.Core.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Data.Seeders;

public class DiscountSeeder
{
    private readonly string _connectionString;

    public DiscountSeeder(IOptions<DatabaseSettings> options)
    {
        _connectionString = options.Value.ConnectionString;
    }

    public async Task SeedAsync()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

      
        var createTableQuery = @"
            CREATE TABLE IF NOT EXISTS Coupons (
                Id SERIAL PRIMARY KEY,
                ProductId TEXT NOT NULL UNIQUE,
                ProductName VARCHAR(500) NOT NULL,
                Description TEXT,
                Amount DECIMAL,
                Percentage DECIMAL,
                IsActive BOOLEAN DEFAULT TRUE,
                ExpiresAt TIMESTAMP
            )";
        await connection.ExecuteAsync(createTableQuery);

       
        var count = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM Coupons");
        if (count > 0) return;

      
        var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "SeedData", "coupons.json");
        if (!File.Exists(jsonPath))
            throw new FileNotFoundException($"Seed data file not found: {jsonPath}");

        var jsonContent = await File.ReadAllTextAsync(jsonPath);
        var coupons = JsonSerializer.Deserialize<List<Coupon>>(jsonContent);

        if (coupons == null || !coupons.Any()) return;

       
        var query = @"
            INSERT INTO Coupons (ProductId, ProductName, Description, Amount, Percentage, IsActive, ExpiresAt)
            VALUES (@ProductId, @ProductName, @Description, @Amount, @Percentage, @IsActive, @ExpiresAt)";
        await connection.ExecuteAsync(query, coupons);
    }
}