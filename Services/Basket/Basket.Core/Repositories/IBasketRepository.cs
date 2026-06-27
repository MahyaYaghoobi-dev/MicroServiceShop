using Basket.Core.Entities;

namespace Basket.Core.Repositories;

public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName,CancellationToken cancellationToken = default);
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart cart,CancellationToken cancellationToken=default);
    Task<bool> DeleteBasketAsync(string userName,CancellationToken cancellationToken = default);
}