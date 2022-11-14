using System.ComponentModel.DataAnnotations;

namespace AestheticsLifeUI.DataAccess.Models.Registration;

public class RegistrationVm
{
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }

    [Compare("Password")]
    public string ConfirmPassword { get; set; }
}