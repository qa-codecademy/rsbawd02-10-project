using Lamazon.Services.ViewModels.Product;

namespace Lamazon.Services.ViewModels.OrderItem;

public class OrderItemViewModel
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public ProductViewModel Product { get; set; }
    public int Qty { get; set; }
    public decimal Price { get; set; }
}
