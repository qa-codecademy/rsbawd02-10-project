using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public void CreateOrder(CreateOrderViewModel order)
    {
        Order newOrder = new Order()
        {
            OrderDate = DateTime.UtcNow,
            IsActive = true,
            OrderNumber = $"{DateTime.UtcNow.ToLongTimeString()}_{order.UserId}",
            UserId = order.UserId
        };

        _orderRepository.Insert(newOrder);
    }

    public OrderViewModel GetActiveOrder(int userId)
    {
        Order activeOrder = _orderRepository.GetActiveOrder(userId);

        OrderViewModel activeOrderViewModel = null;

        if (activeOrder != null)
        {
            activeOrderViewModel = new OrderViewModel()
            {
                Id = activeOrder.Id,
                CreatedDate = activeOrder.OrderDate,
                OrderNumber= activeOrder.OrderNumber,
                UserId = userId,
                User = new ViewModels.User.UserViewModel()
                {
                    FullName = activeOrder.User.FirstName + " " + activeOrder.User.LastName
                }
            };
        }

        return activeOrderViewModel;
    }

    public List<OrderViewModel> GetAllOrders(int userId)
    {
        throw new NotImplementedException();
    }

    public OrderViewModel GetOrderById(int id)
    {
        throw new NotImplementedException();
    }
}
