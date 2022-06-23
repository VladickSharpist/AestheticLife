using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Auth.Services.Abstractions.Models.Records;
using AestheticLife.DataAccess.Domain.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface ITokenService
{

    Task<TokenDto> RefreshAsync(string refreshToken);

    Task<RefreshTokenRecord> GenerateRefreshTokenAsync(User user);

    TToken DecodeToken<TToken>(string token);

    string EncodeToken(object tokenDto);

    Task<AccessTokenRecord> SetAccessTokenAsync(User user);

}