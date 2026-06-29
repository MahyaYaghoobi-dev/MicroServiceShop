using System.Text.Json;
using Basket.Core.Entities;
using Basket.Core;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Infrastructure.Services;

public class BasketRepository(IDistributedCache redis) : IBasketRepository
{
    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await redis.GetStringAsync(userName, cancellationToken);
        return basket is null ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(
        ShoppingCart cart,
        DateTime? expectedLastUpdated = null,
        CancellationToken cancellationToken = default)
    {
        if (cart is null || string.IsNullOrEmpty(cart.UserName))
            return null;

        if (expectedLastUpdated.HasValue)
        {
            var existing = await GetBasketAsync(cart.UserName, cancellationToken);
            if (existing is null || existing.LastUpdated != expectedLastUpdated.Value)
                return null;
        }

        cart.LastUpdated = DateTime.UtcNow;

        var json = JsonSerializer.Serialize(cart);
        await redis.SetStringAsync(
            cart.UserName,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(3)
            },
            cancellationToken
        );

        return cart;
    }

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await redis.GetStringAsync(userName, cancellationToken);
        if (basket is null) return false;

        await redis.RemoveAsync(userName, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteItemAsync(
        string userName,
        string productId,
        DateTime expectedLastUpdated,
        CancellationToken cancellationToken = default)
    {
        var basket = await GetBasketAsync(userName, cancellationToken);
        if (basket is null) return false;

        var item = basket.Items.FirstOrDefault(x => x.ProductId == productId);
        if (item is null) return false;

        basket.Items.Remove(item);

        var updated = await UpdateBasketAsync(basket, expectedLastUpdated, cancellationToken);
        return updated is not null;
    }
}