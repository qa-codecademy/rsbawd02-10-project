using Lamazon.Services.ViewModels.User;

namespace Lamazon.Services.ViewModels.Order;

public class OrderViewModel
{
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UserId { get; set; }
    public UserViewModel User { get; set; } 
}
