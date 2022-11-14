using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Auth.Services.Abstractions.Models.Records;
using DataAccess.Auth.Abstractions.Models;

namespace AestheticLife.Auth.Services.Abstractions.Interfaces;

public interface ITokenService
{

    Task<TokenDto> RefreshAsync(string refreshToken);

    Task<RefreshTokenRecord> GenerateRefreshTokenAsync(AuthUser user);

    TToken DecodeToken<TToken>(string token);

    string EncodeToken(object tokenDto);

    Task<AccessTokenRecord> SetAccessTokenAsync(AuthUser user);

}