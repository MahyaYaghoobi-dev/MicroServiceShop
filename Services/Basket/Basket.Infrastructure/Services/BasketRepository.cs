using System.Text.Json;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Infrastructure.Services;

public class BasketRepository(IDistributedCache redis):IBasketRepository
{
    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
    {
        var basket = await redis.GetStringAsync(userName, cancellationToken);  
        if (basket == null) return null;
        
        var deserializedBasket=JsonSerializer.Deserialize<ShoppingCart>(basket);
        return deserializedBasket;
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
    {
        if (cart is null || string.IsNullOrEmpty(cart.UserName))
            return null;

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
}