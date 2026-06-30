using Discount.Core.Entities;

namespace Discount.Core.Repositories;

public interface IDiscountRepository
{
    Task<Coupon?> GetDiscountAsync(string productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Coupon>?> GetAllDiscountsAsync(CancellationToken cancellationToken = default);
    Task<bool> CreateDiscountAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task<bool> UpdateDiscountAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task<bool> DeleteDiscountAsync(string productId, CancellationToken cancellationToken = default);
}