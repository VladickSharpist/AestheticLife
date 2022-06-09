namespace AestheticLife.Auth.Services.Abstractions.Models;

public class ConfirmUserEmailDto
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}