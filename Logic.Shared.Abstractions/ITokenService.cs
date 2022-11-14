using System.Security.Claims;
using Logic.Shared.Abstractions.Records;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Shared.Abstractions;

public interface ITokenService
{
    Task<RefreshTokenRecord> GenerateRefreshTokenAsync(long id);

    TToken DecodeToken<TToken>(string token);

    string EncodeToken(object tokenDto);
    TokenOptionsRecord GenerateJwtTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
    SigningCredentials GetJwtSigningCredentials();
}