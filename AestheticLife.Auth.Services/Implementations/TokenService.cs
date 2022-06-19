using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.Core.Abstractions.Helpers;
using AestheticLife.DataAccess.Domain.Models;
using AestheticLife.DataAccess.Extensions;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace AestheticLife.Auth.Services.Implementations;

internal class TokenService : ITokenService
{
    private UserManager<User> _userManager;
    private IConfigurationHelper _configurationHelper;

    public TokenService(
        UserManager<User> userManager, IConfigurationHelper configurationHelper)
    {
        _userManager = userManager;
        _configurationHelper = configurationHelper;
    }

    public async Task<TokenDto> RefreshAsync(string refreshToken)
    {
        var decodedToken = DecodeToken<RefreshTokenDto>(refreshToken);
        var user = await _userManager.FindByEmailAsync(decodedToken.UserEmail);
        var decodedActualUserRefreshToken = DecodeToken<RefreshTokenDto>(user.ActualRefreshToken);
        if (decodedToken.IsExpired
            || user is null
            || decodedActualUserRefreshToken.IsExpired
            || refreshToken != user.ActualRefreshToken)
            throw new Exception("Invalid refresh token");

        return new()
        {
            RefreshToken = await GenerateRefreshTokenAsync(user),
            AccessToken = await SetAccessTokenAsync(user)
        };
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var refreshToken = new RefreshTokenDto
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Roles = userRoles,
            ExpiresInMinutes = DateTime.Now.AddMinutes(30)
        };

        return await _userManager.SetActiveRefreshTokenAsync(user, EncodeToken(refreshToken));
    }

    public string EncodeToken(object tokenDto)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(tokenDto));
        return Convert.ToBase64String(plainTextBytes);
    }

    public async Task<string> SetAccessTokenAsync(User user)
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
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
    private async Task<List<Claim>> GetClaims(User user)
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

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = _configurationHelper.JwtConfig;
        var tokenOptions = new JwtSecurityToken
        (
            issuer: jwtSettings["ValidIssuer"],
            audience: jwtSettings["ValidAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresIn"])),
            signingCredentials: signingCredentials
        );
        return tokenOptions;
    }
}