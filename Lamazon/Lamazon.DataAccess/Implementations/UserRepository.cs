using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Implementations;

public class UserRepository : IUserRepository
{
    private readonly LamazonDBContext _dbContext;   
    public UserRepository(LamazonDBContext lamazonDBContext)
    {
        _dbContext = lamazonDBContext;
    }

    public int Insert(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return user.Id;
    }
}
