using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lamazon.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult UserOrders()
        {

            // HOMEWORK
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult ShoppingCart()
        {
            string userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdValue);

            OrderViewModel activeOrderData = _orderService.GetActiveOrder(userId);

            return View(activeOrderData);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Summary() 
        {
            string userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdValue);

            OrderViewModel activeOrderData = _orderService.GetActiveOrder(userId);

            return View(activeOrderData);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Summary([FromForm] OrderViewModel model)
        {
            _orderService.SubmitOrder(model);

            // TODO ADD PAYMENT METHOD USING STRIPE

            return RedirectToAction("Confirmation", new { orderNumber = model.OrderNumber});
        }

        [HttpGet]
        [Authorize]
        public IActionResult Confirmation(string orderNumber) 
        {
            ViewData["orderNumber"] = orderNumber;
            return View();
        }
    }
}
