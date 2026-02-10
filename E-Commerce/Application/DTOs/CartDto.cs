namespace E_Commerce.Application.DTOs;

public class CartDto
{
    public List<CartItemDto> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
}

public class CartItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}
