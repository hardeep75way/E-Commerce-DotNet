using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces.Repositories;
using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Application.Mappers;
using E_Commerce.Application.Validators;
using E_Commerce.Domain.Entities;
using E_Commerce.Infrastucture.Data;

namespace E_Commerce.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repo;
    private readonly AppDbContext _context;

    public ProductService(IProductRepository repo, AppDbContext context)
    {
        _repo = repo;
        _context = context;
    }

    public async Task AddProductAsync(CreateProductDto dto)
    {
        var product = ProductMapper.ToEntity(dto);
        await _repo.AddAsync(product);
        await _context.SaveChangesAsync();
    }
    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await _repo.GetByIdAsync(id);

        if (product == null)
            throw new KeyNotFoundException($"Product with ID {id} not found.");

        return product;
    }
    public async Task UpdateAsync(UpdateProductDto dto)
    {
        var product = await _repo.GetByIdAsync(dto.Id);

        if (product == null)
        {
            throw new KeyNotFoundException(
                $"Product with ID {dto.Id} not found."
            );
        }

        ProductMapper.MapForUpdate(product, dto);

        await _repo.UpdateAsync(product);
        await _context.SaveChangesAsync();
    }
    public async Task<ProductListDto> GetPagedAsync(
        int pageNumber,
        int pageSize,
        string? search,
        string? category)
    {
        if (pageNumber <= 0)
            pageNumber = 1;

        if (pageSize <= 0)
            pageSize = 10;

        var products = await _repo.GetPagedAsync(
            pageNumber, pageSize, search, category);

        var totalCount = await _repo.GetTotalCountAsync(
            search, category);

        return new ProductListDto
        {
            Products = products,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
    public async Task<ExcelUploadResultDto> UploadFromExcelAsync(
        IEnumerable<ExcelProductRowDto> rows)
    {
        var validProducts = new List<Product>();
        var errors = new List<string>();

        foreach (var row in rows)
        {
            if (!ExcelProductRowValidator.IsValid(row, out var error))
            {
                errors.Add($"Row {row.RowNumber}: {error}");
                continue;
            }

            var product = new Product
            {
                Name = row.Name,
                Category = row.Category,
                Price = row.Price,
                StockQuantity = row.StockQuantity,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "Admin"
            };

            validProducts.Add(product);
        }

        if (validProducts.Any())
        {
            await _repo.AddRangeAsync(validProducts);
            await _context.SaveChangesAsync();
        }

        return new ExcelUploadResultDto
        {
            InsertedCount = validProducts.Count,
            Errors = errors
        };
    }


}