using E_Commerce.Domain.Enums;

namespace E_Commerce.Domain.Entities;

public class Order : BaseEntity
{
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}