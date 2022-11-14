using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Auth.Services.Abstractions.Models.Records;
using AestheticLife.Core.Abstractions.Helpers;
using DataAccess.Auth.Abstractions.Models;
using DataAccess.Auth.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AestheticLife.Auth.Services.Implementations;

internal class TokenService : ITokenService
{
    private UserManager<AuthUser> _userManager;
    private IConfigurationHelper _configurationHelper;

    public TokenService(
        UserManager<AuthUser> userManager, IConfigurationHelper configurationHelper)
    {
        _userManager = userManager;
        _configurationHelper = configurationHelper;
    }

    public async Task<TokenDto> RefreshAsync(string refreshToken)
    {
        var decodedToken = DecodeToken<RefreshTokenDto>(refreshToken);
        var user = await _userManager.FindByIdAsync(decodedToken.UserId.ToString());
        var decodedActualUserRefreshToken = DecodeToken<RefreshTokenDto>(user.ActualRefreshToken);
        if (decodedToken.IsExpired
            || user is null
            || decodedActualUserRefreshToken.IsExpired
            || refreshToken != user.ActualRefreshToken)
            throw new Exception("Invalid refresh token");
        var refreshTokenRecord = await GenerateRefreshTokenAsync(user);
        var accessTokenRecord = await SetAccessTokenAsync(user);
        return new()
        {
            RefreshToken = refreshTokenRecord.refreshToken,
            AccessToken = accessTokenRecord.accessToken,
            RefreshTokenExpiresAt = refreshTokenRecord.expiresAt,
            AccessTokenExpiresAt = accessTokenRecord.expiresAt
        };
    }

    public async Task<RefreshTokenRecord> GenerateRefreshTokenAsync(AuthUser user)
    {
        var refreshToken = new RefreshTokenDto
        {
            UserId = user.Id,
            ExpiresInMinutes = DateTime.Now.AddMinutes(30)
        };
        var encodedToken = await _userManager.SetActiveRefreshTokenAsync(user, EncodeToken(refreshToken));
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

    public async Task<AccessTokenRecord> SetAccessTokenAsync(AuthUser user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptionsRecord = GenerateTokenOptions(signingCredentials, claims);
        var encodedAccessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptionsRecord.token);
        return new(encodedAccessToken, tokenOptionsRecord.expiresAt);
    }

    public TToken DecodeToken<TToken>(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var decodedToken = JsonSerializer.Deserialize<TToken>(
            Encoding.UTF8.GetString(base64EncodedBytes));
        return decodedToken;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var jwtConfig = _configurationHelper.JwtConfig;
        var key = Encoding.UTF8.GetBytes(jwtConfig["Secret"]);
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }

    //think of transfer it to userService
    private async Task<List<Claim>> GetClaims(AuthUser user)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Email, user.Email),
            new ("Id", user.Id.ToString())
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        return claims;
    }

    private TokenOptionsRecord GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
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