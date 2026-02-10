using E_Commerce.Application.DTOs.Auth;
using E_Commerce.Domain.Entities;

namespace E_Commerce.Application.Interfaces.Services;

public interface IAuthService
{
    Task<User?> LoginAsync(LoginDto loginDto);
    Task<User> RegisterAsync(RegisterDto registerDto);
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}
