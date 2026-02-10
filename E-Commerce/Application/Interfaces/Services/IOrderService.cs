using E_Commerce.Application.DTOs;

namespace E_Commerce.Application.Interfaces.Services;

/// <summary>
/// Service interface for managing orders and checkout process
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Process checkout: validate stock, reduce quantities, create order
    /// All operations performed in a single database transaction
    /// </summary>
    Task<OrderDto> CheckoutAsync(List<CartItemDto> cartItems);
    
    /// <summary>
    /// Retrieve order by ID with all order items
    /// </summary>
    Task<OrderDto?> GetOrderByIdAsync(int id);
    
    /// <summary>
    /// Retrieve all orders ordered by date descending
    /// </summary>
    Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
}
