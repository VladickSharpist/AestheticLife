using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Models;

public class AuthUser : IdentityUser<long>, IEntity
{
    public IEnumerable<UserRole> UserRoles { get; set; }

    public string ActualRefreshToken { get; set; }
}