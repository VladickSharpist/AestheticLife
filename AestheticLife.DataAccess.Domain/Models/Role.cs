using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class Role : IdentityRole<long>
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}