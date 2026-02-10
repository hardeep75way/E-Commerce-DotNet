using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Repositories;

public interface IProductRepository
{
    
        Task AddAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task UpdateAsync(Product product);
        Task<IReadOnlyList<Product>> GetPagedAsync(
                int pageNumber,
                int pageSize,
                string? search,
                string? category
        );

        Task<int> GetTotalCountAsync(
                string? search,
                string? category
        );
        
        Task AddRangeAsync(IEnumerable<Product> products);
    
}