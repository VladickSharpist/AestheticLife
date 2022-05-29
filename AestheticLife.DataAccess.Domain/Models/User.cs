using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class User : IdentityUser<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}