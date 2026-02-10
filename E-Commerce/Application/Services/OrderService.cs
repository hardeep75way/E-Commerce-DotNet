using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces.Repositories;
using E_Commerce.Application.Interfaces.Services;
using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Enums;
using E_Commerce.Infrastucture.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly AppDbContext _context;

    public OrderService(
        IOrderRepository orderRepository, 
        IProductRepository productRepository,
        AppDbContext context)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _context = context;
    }

 
    public async Task<OrderDto> CheckoutAsync(List<CartItemDto> cartItems)
    {
        if (cartItems == null || !cartItems.Any())
        {
            throw new InvalidOperationException("Cart is empty");
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                // Valiadting stock for all the items
                foreach (var item in cartItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException($"Product {item.ProductName} not found");
                    }
                    
                    if (product.StockQuantity < item.Quantity)
                    {
                        throw new InvalidOperationException(
                            $"Insufficient stock for {product.Name}. Available: {product.StockQuantity}, Requested: {item.Quantity}");
                    }
                }
                
                //  Reducing stock for all items
                foreach (var item in cartItems)
                {
                    var product = await _productRepository.GetByIdAsync(item.ProductId);
                    product.StockQuantity -= item.Quantity;
                    await _productRepository.UpdateAsync(product);
                }
                
                // Order Creation
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Pending,
                    Items = cartItems.Select(x => new OrderItem 
                    {
                        ProductId = x.ProductId,
                        ProductName = x.ProductName,
                        Price = x.Price,
                        Quantity = x.Quantity,
                        Subtotal = x.Subtotal
                    }).ToList()
                };
                
                order.TotalAmount = order.Items.Sum(x => x.Subtotal);
                
                await _orderRepository.CreateOrderAsync(order);
                
                // Commit transaction 
                await transaction.CommitAsync();
                
                return order.ToDto();
            }
            catch
            {
                // Rollback transaction 
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    
    public async Task<OrderDto?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        return order?.ToDto();
    }

   
    public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllAsync();
        return orders.Select(o => o.ToDto());
    }
}


public static class OrderExtensions
{
    public static OrderDto ToDto(this Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = (int)order.Status,
            Items = order.Items.Select(i => i.ToDto()).ToList()
        };
    }

    public static OrderItemDto ToDto(this OrderItem item)
    {
        return new OrderItemDto
        {
            Id = item.Id,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Price = item.Price,
            Quantity = item.Quantity,
            Subtotal = item.Subtotal
        };
    }
}
