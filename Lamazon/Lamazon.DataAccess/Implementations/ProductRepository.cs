using Lamazon.DataAccess.Context;
using Lamazon.DataAccess.Interfaces;
using Lamazon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lamazon.DataAccess.Implementations;

public class ProductRepository : IProductRepository
{
    private readonly LamazonDBContext _dbContext;

    public ProductRepository(LamazonDBContext lamazonDBContext)
    {
        _dbContext = lamazonDBContext;
    }

    public void Delete(int id)
    {
        Product product = _dbContext
            .Products
            .FirstOrDefault(x => x.Id == id);

        _dbContext
            .Products
            .Remove(product);

        _dbContext.SaveChanges();
    }

    public Product Get(int id)
    {
        Product product = _dbContext
            .Products
            .Include(p => p.ProductCategory)
            .Where(p => p.Id == id)
            .FirstOrDefault();

        return product;
    }

    public List<Product> GetAll()
    {
        return _dbContext
            .Products
            .Include(p => p.ProductCategory)
            .ToList();
    }

    public int Insert(Product product)
    {
        _dbContext.Products.Add(product);
        _dbContext.SaveChanges();
        return product.Id;
    }

    public void Update(Product product)
    {
        _dbContext.Products.Update(product);
        _dbContext.SaveChanges();
    }
}
