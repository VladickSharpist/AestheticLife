using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Extensions;

public static class UserManagerExtensions
{
    public static async Task<string> SetActiveRefreshTokenAsync(
        this UserManager<AuthUser> userManager,
        AuthUser user,
        string refreshToken)
    {
        user.ActualRefreshToken = refreshToken;
        await userManager.UpdateAsync(user);
        return refreshToken;
    }
}