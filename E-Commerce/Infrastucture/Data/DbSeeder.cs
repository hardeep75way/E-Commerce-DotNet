using E_Commerce.Domain.Entities;
using E_Commerce.Infrastucture.Data;

namespace E_Commerce.Infrastucture.Data;


public static class DbSeeder
{
  
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.Products.Any())
        {
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Category = "Electronics",
                    Price = 999.99m,
                    StockQuantity = 10
                },
                new Product
                {
                    Name = "Wireless Mouse",
                    Category = "Electronics",
                    Price = 29.99m,
                    StockQuantity = 50
                },
                new Product
                {
                    Name = "Mechanical Keyboard",
                    Category = "Electronics",
                    Price = 89.99m,
                    StockQuantity = 25
                },
                new Product
                {
                    Name = "USB-C Cable",
                    Category = "Accessories",
                    Price = 12.99m,
                    StockQuantity = 100
                },
                new Product
                {
                    Name = "Monitor 27 inch",
                    Category = "Electronics",
                    Price = 349.99m,
                    StockQuantity = 15
                },
                new Product
                {
                    Name = "Webcam HD",
                    Category = "Electronics",
                    Price = 79.99m,
                    StockQuantity = 30
                },
                new Product
                {
                    Name = "Desk Lamp",
                    Category = "Furniture",
                    Price = 39.99m,
                    StockQuantity = 40
                },
                new Product
                {
                    Name = "Office Chair",
                    Category = "Furniture",
                    Price = 199.99m,
                    StockQuantity = 20
                },
                new Product
                {
                    Name = "Notebook",
                    Category = "Stationery",
                    Price = 4.99m,
                    StockQuantity = 200
                },
                new Product
                {
                    Name = "Pen Set",
                    Category = "Stationery",
                    Price = 9.99m,
                    StockQuantity = 150
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
}
