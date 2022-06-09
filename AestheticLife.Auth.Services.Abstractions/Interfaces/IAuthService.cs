using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Helpers;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterUserDto userDto);
    
    Task<string> LoginAsync(LoginDto dto);
}