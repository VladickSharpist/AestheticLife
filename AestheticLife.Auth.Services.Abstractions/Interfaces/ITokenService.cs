using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface ITokenService
{

    Task<TokenDto> RefreshAsync(string refreshToken);

    Task<string> GenerateRefreshTokenAsync(User user);

    TToken DecodeToken<TToken>(string token);

    string EncodeToken(object tokenDto);

    Task<string> SetAccessTokenAsync(User user);

}