using E_Commerce.Application.DTOs;

namespace E_Commerce.Models.Customer.Cart;

public static class CartExtensions
{
    public static CartViewModel ToViewModel(this CartDto dto)
    {
        return new CartViewModel
        {
            Items = dto.Items.Select(x => x.ToViewModel()).ToList(),
            TotalAmount = dto.TotalAmount
        };
    }

    public static CartItemViewModel ToViewModel(this CartItemDto dto)
    {
        return new CartItemViewModel
        {
            ProductId = dto.ProductId,
            ProductName = dto.ProductName,
            Price = dto.Price,
            Quantity = dto.Quantity,
            Subtotal = dto.Subtotal
        };
    }
}
