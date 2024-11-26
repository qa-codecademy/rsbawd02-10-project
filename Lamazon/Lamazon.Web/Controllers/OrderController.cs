using Lamazon.Services.Implementations;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Order;
using Lamazon.Services.ViewModels.OrderItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Stripe.Checkout;
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
            try
            {
                _orderService.SubmitOrder(model);

                string userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                int userId = int.Parse(userIdString);

                OrderViewModel activeOrder = _orderService.GetActiveOrder(userId);

                // TODO ADD PAYMENT METHOD USING STRIPE

                // Add Stripe option
                string domain = "https://localhost:7265";

                SessionCreateOptions stripePaymentSession = new SessionCreateOptions()
                {
                    Mode = "payment",
                    LineItems = new List<SessionLineItemOptions>(),

                    SuccessUrl = $"{domain}/Order/Confirmation?orderId={model.Id}",
                    CancelUrl = $"{domain}/Order/ShoppingCart"
                };

                foreach (OrderItemViewModel orderItem in activeOrder.Items)
                {
                    long priceInCents = (long)(orderItem.Price * 100);

                    SessionLineItemOptions productItem = new SessionLineItemOptions()
                    {
                        Quantity = orderItem.Qty,
                        PriceData = new SessionLineItemPriceDataOptions()
                        {
                            UnitAmount = priceInCents,
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions()
                            {
                                Name = orderItem.Product.Name
                            }
                        }
                    };

                    stripePaymentSession.LineItems.Add(productItem);
                }

                SessionService sessionService = new SessionService();
                Session session = sessionService.Create(stripePaymentSession);

                return Redirect(session.Url);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Internal Server Error happened for User {User.FindFirstValue(ClaimTypes.NameIdentifier)}");

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult Confirmation(int orderId)
        {
            OrderViewModel orderViewModel = _orderService.GetOrderById(orderId);
            _orderService.SetInactiveOrder(orderId); 

            ViewData["orderNumber"] = orderViewModel.OrderNumber;
            return View();
        }
    }
}
