namespace Lamazon.Services.Interfaces;

public interface IOrderItemService
{
    void CreateOrderItem(int productId, int orderId);
}
