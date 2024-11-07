using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Implementations;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly LamazonDBContext _dbContext;
    public OrderItemRepository(LamazonDBContext lamazonDBContext)
    {
        _dbContext = lamazonDBContext;
    }

    public void Delete(int id)
    {
        OrderItem orderItem = _dbContext
            .OrderItems
            .FirstOrDefault(oi => oi.Id == id);

        _dbContext.OrderItems.Remove(orderItem);
        _dbContext.SaveChanges();
    }

    public OrderItem Get(int id)
    {
        OrderItem orderItem = _dbContext
            .OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .Where(oi => oi.Id == id)
            .FirstOrDefault();

        return orderItem;
    }

    public List<OrderItem> GetAll()
    {
        return _dbContext
            .OrderItems
            .Include(oi => oi.Order)
            .Include(oi => oi.Product)
            .ToList();
    }

    public int Insert(OrderItem item)
    {
        _dbContext.OrderItems.Add(item);
        _dbContext.SaveChanges();

        return item.Id;
    }

    public void Update(OrderItem item)
    {
        _dbContext.OrderItems.Update(item); 
        _dbContext.SaveChanges();
    }
}
