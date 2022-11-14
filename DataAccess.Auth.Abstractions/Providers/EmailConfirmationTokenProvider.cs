using DataAccess.Auth.Abstractions.Models;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Providers;

public class EmailConfirmationTokenProvider<TUser> : IUserTwoFactorTokenProvider<TUser>
    where TUser : AuthUser
{
    private string GenerateToken(AuthUser user, string purpose)
    {
        var secretString = "Vlad";
        return secretString + user.Email + purpose + user.Id;
    }
    
    
    public Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
        => Task.FromResult(GenerateToken(user, purpose));
    

    public Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
        => Task.FromResult(token == GenerateToken(user, purpose));

    public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
        => Task.FromResult(manager is not null && user is not null);
}