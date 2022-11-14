using AestheticsLife.DataAccess.Shared.Abstractions.Models;

namespace AestheticsLife.DataAccess.User.Abstractions.Models;

public class ApplicationUser : BaseEntity
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public DateTime DateOfBirth { get; set; }
}