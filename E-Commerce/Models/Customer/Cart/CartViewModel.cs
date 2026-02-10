namespace E_Commerce.Models.Customer.Cart;

public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();
    public decimal TotalAmount { get; set; }
    public int ItemCount => Items.Sum(x => x.Quantity);
}
