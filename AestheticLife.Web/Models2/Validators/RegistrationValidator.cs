using AestheticLife.Web.Models2.Request;
using FluentValidation;

namespace AestheticLife.Web.Models2.Validators;

public class RegistrationValidator : AbstractValidator<RegistrationRequestVm> 
{
    public RegistrationValidator()
    {
        RuleFor(r => r.UserName)
            .NotNull()
            .WithErrorCode("Username can not be null");
        RuleFor(r => r.Email)
            .EmailAddress()
            .NotNull()
            .WithErrorCode("Email can not be null");
        RuleFor(r => r.Password)
            .NotNull()
            .WithErrorCode("Password can not be null");
        RuleFor(r => r.Password)
            .Matches(@"^(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z])\S{0,}$")
            .WithErrorCode("Password must contain at least 1 letter, 1 number and 1 special symbol, no spaces");
        RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password)
            .WithErrorCode("Confirm password field should match the password field");
    }
}