using System.ComponentModel.DataAnnotations;

namespace Auth.Service.Models;

public class LoginRequestVm
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}