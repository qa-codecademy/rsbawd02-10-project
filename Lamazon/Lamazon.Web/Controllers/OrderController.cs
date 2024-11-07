using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult UserOrders()
        {

            // HOMEWORK
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ShoppingCart()
        {
            return View();
        }
    }
}
