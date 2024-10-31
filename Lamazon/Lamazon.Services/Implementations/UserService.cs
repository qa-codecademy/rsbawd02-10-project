using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void RegisterUser(RegisterUserViewModel model)
    {
        if (model == null)
            throw new ArgumentNullException("Provided model is NULL");

        if (model.Password != model.ConfirmationPassword)
            throw new ArgumentException("Provided passwords are not equal");

        User user = new User()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Address = "-",
            PhoneNumber = "-",
            City = "-",
            RoleId = 2,
            UserName = model.UserName,
        };

        // TODO
        string hashPassword = "ovo je hash password";

        user.Password = hashPassword;

        _userRepository.Insert(user);
    }
}
