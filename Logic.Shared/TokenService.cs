using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AestheticLife.Core.Abstractions.Helpers;
using Logic.Shared.Abstractions;
using Logic.Shared.Abstractions.Models;
using Logic.Shared.Abstractions.Records;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Shared;

internal class TokenService : ITokenService
{
    private IConfigurationHelper _configurationHelper;

    public TokenService(IConfigurationHelper configurationHelper)
    {
        _configurationHelper = configurationHelper;
    }

    public async Task<RefreshTokenRecord> GenerateRefreshTokenAsync(long id)
    {
        var refreshToken = new RefreshTokenDto
        {
            UserId = id,
            ExpiresInMinutes = DateTime.Now.AddMinutes(30)
        };
        var encodedToken = EncodeToken(refreshToken);
        return new(
            encodedToken, 
            refreshToken.ExpiresInMinutes.ToString("yyyy-MM-ddTHH:mm:ss"));
    }

    public string EncodeToken(object tokenDto)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(tokenDto));
        return Convert.ToBase64String(plainTextBytes);
    }

    public TToken DecodeToken<TToken>(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var decodedToken = JsonSerializer.Deserialize<TToken>(
            Encoding.UTF8.GetString(base64EncodedBytes));
        return decodedToken;
    }

    public SigningCredentials GetJwtSigningCredentials()
    {
        var jwtConfig = _configurationHelper.JwtConfig;
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    public TokenOptionsRecord GenerateJwtTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configurationHelper.JwtConfig;
        var expireTime = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"]));
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: expireTime,
            signingCredentials: signingCredentials
        );
        return new (tokenOptions, expireTime.ToString("yyyy-MM-ddTHH:mm:ss"));
    }
}