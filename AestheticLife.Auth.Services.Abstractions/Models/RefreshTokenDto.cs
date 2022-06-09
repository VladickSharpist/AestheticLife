namespace AestheticLife.Auth.Services.Abstractions.Models;

public class RefreshTokenDto
{
    public long UserId { get; set; }

    public string UserEmail { get; set; }

    public IEnumerable<string> Roles { get; set; }
}