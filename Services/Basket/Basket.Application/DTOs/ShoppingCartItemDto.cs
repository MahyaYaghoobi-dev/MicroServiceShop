namespace Basket.Application.DTOs;

public class ShoppingCartItemDto
{
    
    public string ProductId { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public string? ImageFile { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}