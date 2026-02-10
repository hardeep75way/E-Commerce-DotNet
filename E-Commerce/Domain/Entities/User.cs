using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Domain.Entities;

public class User
{
    public int Id { get; set; }
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
    
}