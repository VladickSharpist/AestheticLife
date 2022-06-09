using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AestheticLife.Auth.Services.Abstractions.Interfaces;
using AestheticLife.Auth.Services.Abstractions.Models;
using AestheticLife.DataAccess.Domain.Models;
using AestheticLife.DataAccess.Extensions;
using AestheticsLife.DataAccess.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.Auth.Services.Implementations;

internal class TokenService : ITokenService
{
    private UserManager<User> _userManager;
    private IUnitOfWork _unitOfWork;

    public TokenService(
        UserManager<User> userManager,
        IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    public async Task<object> RefreshAsync(string refreshToken)
    {
        return new { refreshToken = refreshToken, accessToken = "asdadasdasdad"};
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var refreshToken = new RefreshTokenDto
        {
            UserId = user.Id,
            UserEmail = user.Email,
            Roles = userRoles
        };

        return EncodeRefreshToken(refreshToken);
    }
    
    public string EncodeRefreshToken(RefreshTokenDto tokenDto)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(
            JsonSerializer.Serialize(tokenDto));
        return Convert.ToBase64String(plainTextBytes);
    }
    
    public RefreshTokenDto DecodeRefreshToken(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var decodedToken = JsonSerializer.Deserialize<RefreshTokenDto>(
            Encoding.UTF8.GetString(base64EncodedBytes));
        return decodedToken;
    }
}