using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;

namespace Lamazon.Services.Implementations;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;

    public OrderItemService(IOrderRepository orderRepository, 
        IOrderItemRepository orderItemRepository,
        IProductRepository productRepository)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    public void CreateOrderItem(int productId, int orderId)
    {
        Order order = _orderRepository.Get(orderId);

        OrderItem existingOrderItem = order
            .Items
            .Where(x => x.ProductId == productId)
            .Where(x => x.OrderId == orderId)
            .FirstOrDefault();

        if (existingOrderItem != null)
        {
            existingOrderItem.Quantity++;
            _orderItemRepository.Update(existingOrderItem);

            return;
        }

        Product product = _productRepository.Get(productId);

        OrderItem orderItem = new OrderItem()
        {
            ProductId = productId,
            OrderId = orderId,
            Price = product.Price,
            Quantity = 1
        };

        _orderItemRepository.Insert(orderItem);
    }
}
