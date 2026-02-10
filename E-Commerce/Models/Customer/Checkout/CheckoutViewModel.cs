namespace E_Commerce.Models.Customer.Checkout;

public class CheckoutViewModel
{
    public List<CheckoutItemViewModel> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public int TotalItems => Items.Sum(x => x.Quantity);
}

public class CheckoutItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}
