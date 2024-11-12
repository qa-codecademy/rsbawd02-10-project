using Lamazon.Services.ViewModels.OrderItem;
using Lamazon.Services.ViewModels.User;

namespace Lamazon.Services.ViewModels.Order;

public class OrderViewModel
{
    public OrderViewModel()
    {
        Items = new List<OrderItemViewModel>();
    }

    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
    public UserViewModel User { get; set; }
    
    public List<OrderItemViewModel> Items { get; set; }

    public decimal TotalPrice { get; set; }

    // Shipping Details
    public string? ShippingUserFullName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public string? PhoneNumber { get; set; }
}
