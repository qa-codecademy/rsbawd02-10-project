using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly LamazonDBContext _dbContext;

        public OrderRepository(LamazonDBContext lamazonDBContext)
        {
            _dbContext = lamazonDBContext;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Order Get(int id)
        {
            Order order = _dbContext
                .Orders
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == id);

            return order;
        }

        public Order GetActiveOrder(int userId)
        {
            Order order = _dbContext
              .Orders
              .Include(o => o.User)
              .Where(o => o.IsActive == true)
              .Where(o => o.UserId == userId)
              .FirstOrDefault();

            return order;
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Insert(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();

            return order.Id;
        }

        public void Update(Order order)
        {
            _dbContext
                .Orders
                .Update(order);
            
            _dbContext.SaveChanges();
        }
    }
}
