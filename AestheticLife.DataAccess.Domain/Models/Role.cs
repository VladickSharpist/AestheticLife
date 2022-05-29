using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class Role : IdentityRole<long>
{
    public ICollection<UserRole> UserRoles { get; set; }
}