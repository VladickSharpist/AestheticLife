using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface ITokenService
{
    Task<object> RefreshAsync(string refreshToken);

    Task<string> GenerateRefreshTokenAsync(User user);

    RefreshTokenDto DecodeRefreshToken(string token);

    string EncodeRefreshToken(RefreshTokenDto tokenDto);
}