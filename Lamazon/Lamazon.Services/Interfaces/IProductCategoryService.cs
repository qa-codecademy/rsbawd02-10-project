using Lamazon.Services.ViewModels;

namespace Lamazon.Services.Interfaces;

public interface IProductCategoryService
{
    List<ProductCategoryViewModel> GetAllProductCategories();
    ProductCategoryViewModel GetProductCategoryById(int id);
    void CreateProductCategory(ProductCategoryViewModel productCategory);
    void UpdateProductCategory(ProductCategoryViewModel productCategory);
    void DeleteProductCategory(int id);
}
