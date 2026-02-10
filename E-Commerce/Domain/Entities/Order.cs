namespace E_Commerce.Domain.Entities;

public class Order:BaseEntity
{
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}