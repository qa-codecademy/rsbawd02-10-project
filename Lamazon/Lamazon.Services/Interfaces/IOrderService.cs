using Lamazon.Services.ViewModels.Order;

namespace Lamazon.Services.Interfaces;

public interface IOrderService
{
    List<OrderViewModel> GetAllOrders(int userId);
    OrderViewModel GetOrderById(int id);
    void CreateOrder(CreateOrderViewModel order);
    OrderViewModel GetActiveOrder(int userId);

    OrderViewModel SubmitOrder(OrderViewModel order);

    void SetInactiveOrder(int id);
}
