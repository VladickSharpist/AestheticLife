using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class Role : IdentityRole<long>, IEntity
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}