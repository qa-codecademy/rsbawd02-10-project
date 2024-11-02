using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;
using Lamazon.Services.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lamazon.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService, 
            IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }

        public IActionResult Index()
        {
            List<ProductViewModel> allProducts = _productService.GetAllProducts(); 
            return View(allProducts);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create() 
        { 
            CreateProductViewModel model = new CreateProductViewModel();
            List<ProductCategoryViewModel> allProductCategories =
                _productCategoryService.GetAllProductCategories();

            ViewBag.ProductCategories = new SelectList(allProductCategories, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create([FromForm] CreateProductViewModel model)
        {
            try
            {
                _productService.CreateProduct(model);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // todo
                return null;
            }
        }
    }
}
