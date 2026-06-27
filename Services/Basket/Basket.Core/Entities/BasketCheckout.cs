using System.Text.Json.Serialization;

namespace Basket.Core.Entities;

public class BasketCheckout
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string UserName { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }

    //  User Info 
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // Address
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;

    // Payment 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentMethod PaymentMethod { get; set; }
    public DateTime? PaymentDate { get; set; }
    public string? BankTrackingCode { get; set; }   

    //  Order 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    //  Shipping 
    public string? ShippingTrackingCode { get; set; }  
    public DateTime? ShippedAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}

public enum OrderStatus
{
    Pending,     
    Paid,        
    Processing,  
    Shipped,      
    Delivered,    
    Cancelled    
}

public enum PaymentMethod
{
    CreditCard,
    PayPal,
    COD,
    BankTransfer
}