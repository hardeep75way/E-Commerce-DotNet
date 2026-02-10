using E_Commerce.Application.DTOs;

namespace E_Commerce.Application.Interfaces.Services;


public interface IOrderService
{
    
    Task<OrderDto> CheckoutAsync(List<CartItemDto> cartItems);
    
   
    Task<OrderDto?> GetOrderByIdAsync(int id);
    
   
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
}
