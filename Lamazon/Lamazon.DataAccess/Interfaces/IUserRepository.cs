using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces;

public interface IUserRepository
{
    int Insert(User user);
}
