using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models.Admin.Product;

public class EditProductViewModel
{
    public int Id { get; set; }
    
    [Display(Name = "Product Name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Category")]
    [Required]
    public string Category { get; set; } = string.Empty;

    [Display(Name = "Price")]
    [Required]
    public decimal Price { get; set; }

    [Display(Name = "Stock Quantity")]
    [Required]
    public int StockQuantity { get; set; }
}