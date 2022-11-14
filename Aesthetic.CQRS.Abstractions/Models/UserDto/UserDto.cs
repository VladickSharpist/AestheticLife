namespace Aesthetic.CQRS.Abstractions.Models.UserDto;

public class UserDto
{
    public long Id { get; set; }
    
    public string Name { get; set; }

    public string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
}