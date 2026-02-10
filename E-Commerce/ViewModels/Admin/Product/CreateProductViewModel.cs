using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.Admin.Product;

public class CreateProductViewModel
{
    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Product name is required")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Category")]
    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;

    [Display(Name = "Price")]
    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
    public decimal Price { get; set; }

    [Display(Name = "Stock Quantity")]
    [Required(ErrorMessage = "Stock quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative")]
    public int StockQuantity { get; set; }
}