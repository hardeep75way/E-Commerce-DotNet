using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.DTOs;

public class ProductListDto
{
    public IReadOnlyList<Product> Products { get; set; }
        = new List<Product>();

    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

}