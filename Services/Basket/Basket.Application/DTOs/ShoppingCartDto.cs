namespace Basket.Application.DTOs;

public class ShoppingCartDto
{
    public string UserName { get; set; } = string.Empty;
    public List<ShoppingCartItemDto> Items { get; set; } = new();
    public DateTime LastUpdated { get; set; }  
    public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);
    
}