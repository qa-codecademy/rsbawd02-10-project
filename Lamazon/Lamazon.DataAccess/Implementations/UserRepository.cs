using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lamazon.DataAccess.Implementations;

public class UserRepository : IUserRepository
{
    private readonly LamazonDBContext _dbContext;   
    public UserRepository(LamazonDBContext lamazonDBContext)
    {
        _dbContext = lamazonDBContext;
    }

    public User GetUser(int id)
    {
        return _dbContext
            .Users
            .FirstOrDefault(u => u.Id == id);
    }

    public User GetUserByEmail(string email)
    {
        return _dbContext
            .Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == email);
    }

    public int Insert(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return user.Id;
    }
}
