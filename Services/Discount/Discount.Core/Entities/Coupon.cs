using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.Core.Entities;

[Table("Coupons")]
public class Coupon
{
    public int Id { get; set; }
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal? Amount { get; set; }
    public decimal? Percentage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAt { get; set; }
    public bool IsActive { get; set; } = true;
}