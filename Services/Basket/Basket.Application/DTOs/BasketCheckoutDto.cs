using Basket.Core.Entities;

namespace Basket.Application.DTOs;

public class BasketCheckoutDto
{
    
    public string UserName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    // User Info
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Address
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    // Payment
    public PaymentMethod PaymentMethod { get; set; }
    public string? BankTrackingCode { get; set; }

    // Order
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}