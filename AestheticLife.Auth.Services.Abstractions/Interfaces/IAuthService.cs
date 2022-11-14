using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Helpers;
using Logic.Shared.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface IAuthService
{
    Task<TokenDto> RegisterAsync(RegisterUserDto userDto);
    Task<TokenDto> LoginAsync(LoginDto dto);
    Task<TokenDto> RefreshAsync(string refreshToken);

}