using AestheticLife.DataAccess.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Extensions;

public static class UserManagerExtensions
{
    public static async Task<string> SetActiveRefreshTokenAsync(
        this UserManager<User> userManager,
        User user,
        string refreshToken)
    {
        user.ActualRefreshToken = refreshToken;
        await userManager.UpdateAsync(user);
        return refreshToken;
    }
}