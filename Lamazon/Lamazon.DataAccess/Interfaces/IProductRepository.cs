using Lamazon.Domain.Entities;

namespace Lamazon.DataAccess.Interfaces;

public  interface IProductRepository
{
    List<Product> GetAll();
    Product Get(int id);
    int Insert(Product product);
    void Update(Product product);   
    void Delete(int id);
}
