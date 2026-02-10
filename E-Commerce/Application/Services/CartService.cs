using System.Text.Json;
using E_Commerce.Application.DTOs;
using E_Commerce.Application.Interfaces.Repositories;
using E_Commerce.Application.Interfaces.Services;

namespace E_Commerce.Application.Services;


public class CartService : ICartService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IProductRepository _productRepository;
    private const string CartSessionKey = "ShoppingCart";

    public CartService(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _productRepository = productRepository;
    }
    

    public async Task AddToCartAsync(int productId, int quantity)
    {
        // Validating product exists in database
        var product = await _productRepository.GetByIdAsync(productId);
        if (product == null)
        {
            throw new InvalidOperationException("Product not found");
        }

        var cart = GetCartFromSession();
        var existingItem = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (existingItem != null)
        {
            // Product already in cart 
            existingItem.Quantity += quantity;
            existingItem.Subtotal = existingItem.Price * existingItem.Quantity;
        }
        else
        {
            // New product 
            cart.Items.Add(new CartItemDto
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = quantity,
                Subtotal = product.Price * quantity
            });
        }

        // Recalculate total and save 
        cart.TotalAmount = cart.Items.Sum(x => x.Subtotal);
        SaveCartToSession(cart);
    }

    public async Task<CartDto> GetCartAsync()
    {
        return await Task.FromResult(GetCartFromSession());
    }

    public async Task UpdateQuantityAsync(int productId, int quantity)
    {
        if (quantity < 1)
        {
            throw new InvalidOperationException("Quantity must be at least 1");
        }

        var cart = GetCartFromSession();
        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            item.Quantity = quantity;
            item.Subtotal = item.Price * item.Quantity;
            cart.TotalAmount = cart.Items.Sum(x => x.Subtotal);
            SaveCartToSession(cart);
        }

        await Task.CompletedTask;
    }

    public async Task RemoveFromCartAsync(int productId)
    {
        var cart = GetCartFromSession();
        var item = cart.Items.FirstOrDefault(x => x.ProductId == productId);

        if (item != null)
        {
            cart.Items.Remove(item);
            cart.TotalAmount = cart.Items.Sum(x => x.Subtotal);
            SaveCartToSession(cart);
        }

        await Task.CompletedTask;
    }

    public async Task ClearCartAsync()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session != null)
        {
            session.Remove(CartSessionKey);
        }

        await Task.CompletedTask;
    }

    private CartDto GetCartFromSession()
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session == null)
        {
            return new CartDto();
        }

        var cartJson = session.GetString(CartSessionKey);
        if (string.IsNullOrEmpty(cartJson))
        {
            return new CartDto();
        }

        var cart = JsonSerializer.Deserialize<CartDto>(cartJson);
        return cart ?? new CartDto();
    }

    private void SaveCartToSession(CartDto cart)
    {
        var session = _httpContextAccessor.HttpContext?.Session;
        if (session != null)
        {
            var cartJson = JsonSerializer.Serialize(cart);
            session.SetString(CartSessionKey, cartJson);
        }
    }
}
