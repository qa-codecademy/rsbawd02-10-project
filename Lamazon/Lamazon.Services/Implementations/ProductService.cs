using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void CreateProduct(CreateProductViewModel model)
    {
        if (model == null)
            throw new ArgumentNullException("Provided model is NULL!");

        if (model.ProductCategoryId <= 0)
            throw new ArgumentException("Provided category id is not valid!");

        Product product = new Product()
        {
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            Name = model.Name,
            ProductCategoryId = model.ProductCategoryId,
            Price = model.Price
        };

        int productId = _productRepository.Insert(product);

        if (productId == 0)
            throw new Exception("Something went wrong with insert!");
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }

    public List<ProductViewModel> GetAllProducts()
    {
        return _productRepository
            .GetAll()
            .Select(p => new ProductViewModel()
            {
                Description= p.Description,
                ImageUrl= p.ImageUrl,
                Name = p.Name,
                ProductCategoryId = p.ProductCategoryId,
                Price = p.Price,
                Id = p.Id,
                ProductCategoryName = p.ProductCategory.Name
            }).ToList();
    }

    public ProductViewModel GetProductById(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(UpdateProductViewModel model)
    {
        throw new NotImplementedException();
    }
}
