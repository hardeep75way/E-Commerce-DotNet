using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Mappers;

public static class ProductMapper
{
    public static Product ToEntity(CreateProductDto dto)
    {
        return new Product
        {
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            StockQuantity = dto.StockQuantity
        };
    }
    
    public static void MapForUpdate(
        Product product,
        UpdateProductDto dto)
    {
        product.Name = dto.Name;
        product.Category = dto.Category;
        product.Price = dto.Price;
        product.StockQuantity = dto.StockQuantity;
        product.UpdatedAt = DateTime.UtcNow;
    }
    
}