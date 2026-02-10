using E_Commerce.Application.DTOs;
using E_Commerce.Domain.Enums;

namespace E_Commerce.Models.Customer.Checkout;

public static class CheckoutExtensions
{
    public static CheckoutViewModel ToCheckoutViewModel(this CartDto cart)
    {
        return new CheckoutViewModel
        {
            Items = cart.Items.Select(x => new CheckoutItemViewModel
            {
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal
            }).ToList(),
            TotalAmount = cart.TotalAmount
        };
    }

    public static OrderConfirmationViewModel ToConfirmationViewModel(this OrderDto order)
    {
        return new OrderConfirmationViewModel
        {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            Status = ((OrderStatus)order.Status).ToString(),
            Items = order.Items.Select(x => new OrderItemViewModel
            {
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                Subtotal = x.Subtotal
            }).ToList()
        };
    }
}
