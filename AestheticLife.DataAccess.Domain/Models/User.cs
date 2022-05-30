using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class User : IdentityUser<long>
{
    public IEnumerable<UserRole> UserRoles { get; set; }
}