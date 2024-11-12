using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Lamazon.Services.ViewModels.OrderItem;
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
                OrderNumber = activeOrder.OrderNumber,
                UserId = userId,
                User = new ViewModels.User.UserViewModel()
                {
                    FullName = activeOrder.User.FirstName + " " + activeOrder.User.LastName
                },

                Items = activeOrder.Items
                    .Select(o => new OrderItemViewModel()
                    {
                        Id = o.Id,
                        OrderId = o.OrderId,
                        Price = o.Price,
                        Qty = o.Quantity,
                        Product = new ViewModels.Product.ProductViewModel()
                        {
                            Name = o.Product.Name,
                            Description = o.Product.Description,
                            ImageUrl = o.Product.ImageUrl,
                            Price = o.Product.Price,
                            Id = o.ProductId
                        }
                    })
                    .ToList()
            };

            activeOrderViewModel.TotalPrice = activeOrderViewModel
            .Items
            .Sum(o => o.Price * o.Qty);
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

    public OrderViewModel SubmitOrder(OrderViewModel order)
    {
        Order existingActiveOrder = _orderRepository.Get(order.Id);

        if (existingActiveOrder == null)
            throw new Exception($"There is not existing order with provided ID: {order.Id}");

        existingActiveOrder.ShippingUserFullName = order.ShippingUserFullName;
        existingActiveOrder.Address = order.Address;
        existingActiveOrder.City = order.City;
        existingActiveOrder.PostalCode = order.PostalCode;
        existingActiveOrder.Country = order.Country;
        existingActiveOrder.PhoneNumber = order.PhoneNumber;

        existingActiveOrder.IsActive = false;

        _orderRepository.Update(existingActiveOrder);

        return order;
    }
}
