using Lamazon.Services.ViewModels.Product;

namespace Lamazon.Services.Interfaces;

public interface IProductService
{
    List<ProductViewModel> GetAllProducts();
    ProductViewModel GetProductById(int id);
    void CreateProduct(CreateProductViewModel model);
    void UpdateProduct(UpdateProductViewModel model);
    void DeleteProduct(int id);
}
