using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Implementations;

public class ProductCategoryRepository : IProductCategoryRepository
{
    private readonly LamazonDBContext _dbContext;

    public ProductCategoryRepository(LamazonDBContext context)
    {
        _dbContext = context;
    }

    public void Delete(int id)
    {
        ProductCategory productCategory = _dbContext
            .ProductCategories
            .FirstOrDefault(c => c.Id == id);

        if (productCategory == null)
            throw new Exception($"There is no category with id: {id}");

        _dbContext
            .ProductCategories
            .Remove(productCategory);

        _dbContext.SaveChanges();
    }

    public List<ProductCategory> GetAll()
    {
        List<ProductCategory> productCategories = _dbContext
            .ProductCategories
            .ToList();
    
        return productCategories;
    }

    public ProductCategory GetById(int id)
    {
        ProductCategory productCategory = _dbContext
            .ProductCategories
            .FirstOrDefault(c => c.Id == id);

        if (productCategory == null)
            throw new Exception($"There is no category with id: {id}");
    
        return productCategory;
    }

    public int Insert(ProductCategory category)
    {
        _dbContext
            .ProductCategories
            .Add(category);

        _dbContext.SaveChanges();

        return category.Id;
    }

    public void Update(ProductCategory category)
    {
        _dbContext
            .ProductCategories
            .Update(category);

        _dbContext.SaveChanges();
    }
}