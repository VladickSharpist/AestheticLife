using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AestheticLife.DataAccess.Domain.Models;

public class User : IdentityUser<long>, IEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string SecondName { get; set; }

    public string DateOfBirth { get; set; }

    public string Password { get; set; }


    public IEnumerable<UserRole> UserRoles { get; set; }
    
    public string ActualRefreshToken { get; set; }
}