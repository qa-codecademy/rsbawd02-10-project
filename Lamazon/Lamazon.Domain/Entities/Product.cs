namespace Lamazon.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }

    public int ProductCategoryId { get; set; }
    public ProductCategory ProductCategory { get; set; }
}
