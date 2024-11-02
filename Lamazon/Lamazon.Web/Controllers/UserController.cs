using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [HttpGet]
        public IActionResult Login()
        {
            LoginUserViewModel loginUserViewModel = new LoginUserViewModel();
            return View(loginUserViewModel);
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginUserViewModel model)
        {
            try
            {
                UserViewModel user = _userService.LoginUser(model);

                if (user is null)
                    return BadRequest();

                List<Claim> userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.Role, user.UserRoleKey)
                };

                ClaimsIdentity claimsIdentity 
                    = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync(principal);

                return View("SuccessfullyLogin", user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
