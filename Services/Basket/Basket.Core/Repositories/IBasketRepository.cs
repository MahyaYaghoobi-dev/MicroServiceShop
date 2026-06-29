public interface IBasketRepository
{
    Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
    Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart cart, DateTime? expectedLastUpdated = null, CancellationToken cancellationToken = default);
    Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    Task<bool> DeleteItemAsync(string userName, string productId, DateTime expectedLastUpdated, CancellationToken cancellationToken = default);
}