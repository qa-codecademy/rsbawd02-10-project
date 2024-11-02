using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Lamazon.Web.Controllers;

public class ProductCategoryController : Controller
{

    private readonly IProductCategoryService _productCategoryService;
    
    public ProductCategoryController(IProductCategoryService productCategoryService)
    {
        _productCategoryService = productCategoryService;   
    }

    [HttpGet]
    public IActionResult Index()
    {
        var name = User.Claims.FirstOrDefault(x => x.ValueType == System.Security.Claims.ClaimTypes.Name);

        return View(_productCategoryService.GetAllProductCategories());
    }

    [HttpGet]
    public IActionResult Create() 
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create([FromForm] ProductCategoryViewModel category)
    {
        try
        {
            _productCategoryService.CreateProductCategory(category);
            return RedirectToAction("Index");
        }
        catch (Exception)
        {
            // TODO
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        ProductCategoryViewModel productCategory = 
            _productCategoryService.GetProductCategoryById(id);

        return View(productCategory);
    }

    [HttpGet]
    public IActionResult Delete(int id) 
    {
        _productCategoryService.DeleteProductCategory(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id) 
    {
        ProductCategoryViewModel categoryViewModel = 
            _productCategoryService.GetProductCategoryById(id);

        return View(categoryViewModel);
    }

    [HttpPost]
    public IActionResult Edit([FromForm] ProductCategoryViewModel categoryViewModel)
    {
        try
        {
            _productCategoryService.UpdateProductCategory(categoryViewModel);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // TODO
            return RedirectToAction("Index");
        }
    }
}
