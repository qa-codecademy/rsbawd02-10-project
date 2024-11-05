using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Product;
using Lamazon.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lamazon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
