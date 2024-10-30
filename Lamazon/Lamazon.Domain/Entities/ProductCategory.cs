﻿namespace Lamazon.Domain.Entities;

public class ProductCategory : BaseEntity
{
    public string Name { get; set; }

    public List<Product> Products { get; set; }
}
