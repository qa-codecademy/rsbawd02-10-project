using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Lamazon.Services.Interfaces;
using Lamazon.Services.ViewModels;

namespace Lamazon.Services.Implementations;

public class ProductCategoryService : IProductCategoryService
{
    private readonly IProductCategoryRepository _repository;

    public ProductCategoryService(IProductCategoryRepository repository)
    {
        _repository = repository;
    }

    public void CreateProductCategory(ProductCategoryViewModel productCategory)
    {
        if (productCategory == null)
            throw new ArgumentNullException("The model is NULL");

        if (string.IsNullOrEmpty(productCategory.Name))
            throw new ArgumentException("The name property can not be NULL");

        ProductCategory productCategoryInsert = new ProductCategory()
        {
            Name = productCategory.Name,
        };

        int productCategoryId = _repository.Insert(productCategoryInsert);

        if (productCategoryId <= 0)
            throw new Exception("Something went wrong!");
    }

    public void DeleteProductCategory(int id)
    {
        if (id <= 0)
            throw new Exception("The id must be valid");

        _repository.Delete(id);
    }

    public List<ProductCategoryViewModel> GetAllProductCategories()
    {
        List<ProductCategory> productCategories = _repository.GetAll();

        List<ProductCategoryViewModel> result = productCategories
            .Select(pc => new ProductCategoryViewModel()
            {
                Id = pc.Id,
                Name = pc.Name,
            })
            .ToList();

        return result;
    }

    public ProductCategoryViewModel GetProductCategoryById(int id)
    {
        if (id <= 0)
            throw new Exception("The id must be valid!");

        ProductCategory category = _repository.GetById(id);

        ProductCategoryViewModel productCategoryViewModel = new ProductCategoryViewModel()
        {
            Id = category.Id,
            Name = category.Name,
        };

        return productCategoryViewModel;
    }

    public void UpdateProductCategory(ProductCategoryViewModel productCategory)
    {
        if (productCategory == null)
            throw new ArgumentNullException("The provided value is NULL");

        if (string.IsNullOrEmpty(productCategory.Name))
            throw new Exception("The provided Name value is NULL");

        ProductCategory productCategoryDB = _repository.GetById(productCategory.Id);

        productCategoryDB.Name = productCategory.Name;

        _repository.Update(productCategoryDB);
    }
}
