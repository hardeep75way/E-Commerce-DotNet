using E_Commerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastucture.Data;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<OrderItem> OrderItem => Set<OrderItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }
}
