using Lamazon.Services.ViewModels.User;

namespace Lamazon.Services.Interfaces;

public interface IUserService
{
    void RegisterUser(RegisterUserViewModel model);
    UserViewModel LoginUser(LoginUserViewModel model);
}
