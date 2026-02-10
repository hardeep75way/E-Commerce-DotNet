using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(Order order);
    Task<Order?> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllAsync();
}
