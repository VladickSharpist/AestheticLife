using Microsoft.AspNetCore.Identity;

namespace DataAccess.Auth.Abstractions.Models;

public class UserRole : IdentityUserRole<long>
{
    public AuthUser User { get; set; }

    public Role Role { get; set; }
}