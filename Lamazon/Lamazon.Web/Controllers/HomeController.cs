using AspNetCoreHero.ToastNotification.Abstractions;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Lamazon.Services.ViewModels.Product;
using Lamazon.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Lamazon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly IOrderItemService _orderItemService;
        private readonly IOrderService _orderService;
        private readonly INotyfService _notyfService;

        public HomeController(ILogger<HomeController> logger, 
            IProductService productService,
            IOrderItemService orderItemService,
            IOrderService orderService,
            INotyfService notyfService)
        {
            _logger = logger;
            _productService = productService;
            _orderItemService = orderItemService;
            _orderService = orderService;

            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> allProduct = _productService.GetAllProducts();
            return View(allProduct);
        }

        [HttpGet]
        public IActionResult ProductDetails(int id)
        {
            ProductViewModel model = _productService.GetProductById(id);
            return View(model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddToCart(int productId)
        {
            string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            int userId = int.Parse(userIdString);

            OrderViewModel activeOrder = _orderService.GetActiveOrder(userId);

            if (activeOrder == null)
            {
                CreateOrderViewModel orderViewModel = new CreateOrderViewModel()
                {
                    UserId = userId,
                };

                _orderService.CreateOrder(orderViewModel);

                activeOrder = _orderService.GetActiveOrder(userId);
            }

            _orderItemService.CreateOrderItem(productId, activeOrder.Id);

            _notyfService.Success("Item added to the cart");

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
