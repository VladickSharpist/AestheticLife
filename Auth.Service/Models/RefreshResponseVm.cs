namespace Auth.Service.Models;

public class RefreshResponseVm
{
    public string RefreshToken { get; set; }

    public string AccessToken { get; set; }
    
    public string RefreshTokenExpiresAt { get; set; }

    public string AccessTokenExpiresAt { get; set; }
}