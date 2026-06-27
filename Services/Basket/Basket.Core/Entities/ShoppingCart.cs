namespace Basket.Core.Entities;

public class ShoppingCart(string userName, string userId)
{
    public Guid Guid { get; set; }=Guid.NewGuid();
    public string UserId { get; set; } = userId;
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = [];

    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
}