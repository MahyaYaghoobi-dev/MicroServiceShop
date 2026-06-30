using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Core.Settings;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Discount.Infrastructure.Services;

public class DiscountRepository(IOptions<DatabaseSettings> options):IDiscountRepository
{
    
    private readonly string _connectionString = options.Value.ConnectionString;
    
    public async Task<Coupon?> GetDiscountAsync(string productId, CancellationToken cancellationToken = default)
    {

        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var query = "SELECT * FROM Coupons WHERE ProductId = @ProductId";
            return await connection.QueryFirstOrDefaultAsync<Coupon>(
                query, 
                new { ProductId = productId }
            );
        }
    }

    public async Task<IEnumerable<Coupon>?> GetAllDiscountsAsync(CancellationToken cancellationToken = default)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var query = "SELECT * FROM Coupons";
            return await connection.QueryAsync<Coupon>(query);
        }
    }

    public async Task<bool> CreateDiscountAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var query = "INSERT INTO Coupons (ProductId, ProductName, Description, Amount, Percentage, IsActive, ExpiresAt) VALUES (@ProductId, @ProductName, @Description, @Amount, @Percentage, @IsActive, @ExpiresAt)";
            var result = await connection.ExecuteAsync(query, coupon);
            return result > 0;
        }
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var query = "UPDATE Coupons SET ProductName = @ProductName, Description = @Description, Amount = @Amount, Percentage = @Percentage, IsActive = @IsActive, ExpiresAt = @ExpiresAt WHERE ProductId = @ProductId";
            var result = await connection.ExecuteAsync(query, coupon);
            return result > 0;
        }
    }

    public async Task<bool> DeleteDiscountAsync(string productId, CancellationToken cancellationToken = default)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            var query = "DELETE FROM Coupons WHERE ProductId = @ProductId";
            var result = await connection.ExecuteAsync(query, new { ProductId = productId });
            return result > 0;
        }
    }
}