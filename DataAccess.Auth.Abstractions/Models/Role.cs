using AestheticsLife.DataAccess.Shared.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Models;

public class Role : IdentityRole<long>, IEntity
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}