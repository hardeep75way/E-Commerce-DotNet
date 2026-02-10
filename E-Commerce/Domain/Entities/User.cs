using System.ComponentModel.DataAnnotations;
using E_Commerce.Domain.Enums;

namespace E_Commerce.Domain.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    public UserRole Role { get; set; } = UserRole.Customer;
}