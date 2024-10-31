using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            RegisterUserViewModel registerUserViewModel = new RegisterUserViewModel();
            return View(registerUserViewModel);
        }

        [HttpPost]
        public IActionResult Register([FromForm] RegisterUserViewModel model)
        {
            _userService.RegisterUser(model);
            return View("SuccessRegistration");
        }
    }
}
