using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Models.Customer.Product;

public static class CustomerProductExtensions
{
    public static CustomerProductItemViewModel ToCustomerViewModel(this Domain.Entities.Product product)
    {
        return new CustomerProductItemViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Category = product.Category,
            Price = product.Price,
            StockQuantity = product.StockQuantity
        };
    }

    public static CustomerProductListViewModel ToCustomerListViewModel(
        this ProductListDto dto, 
        int pageNumber, 
        int pageSize)
    {
        return new CustomerProductListViewModel
        {
            Products = dto.Products.Select(p => p.ToCustomerViewModel()),
            TotalCount = dto.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}
