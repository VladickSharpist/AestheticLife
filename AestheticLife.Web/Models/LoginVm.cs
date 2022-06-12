using System.ComponentModel.DataAnnotations;

namespace AestheticLife.Web.Models;

public class LoginVm
{
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}