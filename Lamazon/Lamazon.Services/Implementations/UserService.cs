using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using Microsoft.AspNetCore.Identity;

namespace Lamazon.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public UserViewModel LoginUser(LoginUserViewModel model)
    {
        if (model == null)
            throw new ArgumentNullException("Provided model is null");

        if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            throw new ArgumentException("Provided data is not valid");

        User user = _userRepository.GetUserByEmail(model.Email);

        if (user is null)
            throw new ArgumentNullException("There is no user with that email");

        PasswordVerificationResult verificationResult = 
            _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

        if (verificationResult == PasswordVerificationResult.Failed)
            throw new Exception("Login credentials do not match!");

        return new UserViewModel()
        {
            Id = user.Id,
            Email = user.Email,
            FullName = user.FirstName + " " + user.LastName,
            Username = user.UserName,
            UserRoleKey = user.Role.Key
        };
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

        string hashPassword = _passwordHasher.HashPassword(user, model.Password);
        user.Password = hashPassword;

        _userRepository.Insert(user);
    }
}
