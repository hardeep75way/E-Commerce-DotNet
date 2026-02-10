using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Services;

public interface IProductService
{
    Task AddProductAsync(CreateProductDto dto);
    Task UpdateAsync(UpdateProductDto dto);
    Task<Product> GetByIdAsync(int id);
    Task<ProductListDto> GetPagedAsync(int pageNumber, int pageSize, string? search, string? category);
    Task<ExcelUploadResultDto> UploadFromExcelAsync(IEnumerable<ExcelProductRowDto> rows);
}