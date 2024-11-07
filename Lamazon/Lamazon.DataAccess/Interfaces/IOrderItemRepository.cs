using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces;

public interface IOrderItemRepository
{
    List<OrderItem> GetAll();
    OrderItem Get(int id);
    int Insert(OrderItem item);
    void Update(OrderItem item);
    void Delete(int id);
}
