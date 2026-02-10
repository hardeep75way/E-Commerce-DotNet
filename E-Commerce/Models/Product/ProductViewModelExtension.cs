using E_Commerce.Application.DTOs;


namespace E_Commerce.Models.Product;

public static class ProductViewModelExtension
{
    public static CreateProductDto ToDto(this CreateProductViewModel vm)
    {
        return new CreateProductDto
        {
            Name = vm.Name,
            Category = vm.Category,
            Price = vm.Price,
            StockQuantity = vm.StockQuantity
        };
    }
    public static EditProductViewModel ToEditViewModel(
        this Domain.Entities.Product product)
    {
        return new EditProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }

    public static UpdateProductDto ToDto(
        this EditProductViewModel vm)
    {
        return new UpdateProductDto
        {
            Id = vm.Id,
            Name = vm.Name,
            Category = vm.Category,
            Price = vm.Price,
            StockQuantity = vm.StockQuantity
        };
    }
    
    public static ProductListViewModel ToListViewModel(
        this ProductListDto dto)
    {
        return new ProductListViewModel
        {
            Items = dto.Products.Select(p => new ProductListItemViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                StockQuantity = p.StockQuantity
            }),
            PageNumber = dto.PageNumber,
            PageSize = dto.PageSize,
            TotalCount = dto.TotalCount
        };
    }

}