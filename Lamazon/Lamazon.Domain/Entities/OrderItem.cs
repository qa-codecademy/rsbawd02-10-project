namespace Lamazon.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
