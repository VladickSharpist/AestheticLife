namespace AestheticLife.Auth.Services.Abstractions.Models;

public class TokenDto
{
    public string RefreshToken { get; set; }

    public string AccessToken { get; set; }
}