using E_Commerce.Application.DTOs;

namespace E_Commerce.Application.Interfaces.Services;

public interface ICartService
{
    Task AddToCartAsync(int productId, int quantity);
    Task<CartDto> GetCartAsync();
    Task UpdateQuantityAsync(int productId, int quantity);
    Task RemoveFromCartAsync(int productId);
    Task ClearCartAsync();
}
