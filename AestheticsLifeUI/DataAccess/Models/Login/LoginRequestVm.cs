using System.ComponentModel.DataAnnotations;

namespace AestheticsLifeUI.DataAccess.Models.Login;

public class LoginRequestVm
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}