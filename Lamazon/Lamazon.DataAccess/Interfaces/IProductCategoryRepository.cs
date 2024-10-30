using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces;

public interface IProductCategoryRepository
{
    List<ProductCategory> GetAll();
    ProductCategory GetById(int id);
    int Insert(ProductCategory category);
    void Update(ProductCategory category);
    void Delete(int id);
}
