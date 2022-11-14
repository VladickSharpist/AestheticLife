using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Models;

public class AuthUser : IdentityUser<long>
{
    public string? ActualRefreshToken { get; set; }
}