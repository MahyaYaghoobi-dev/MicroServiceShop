using Basket.Core.Entities;

public class ShoppingCart(string userName)
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = userName;
    public List<ShoppingCartItem> Items { get; set; } = [];
    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
}