using FluentValidation;

namespace AestheticsLife.Web.Training.Models.Request.Validators;

public class AddExerciseValidator : AbstractValidator<AddExerciseRequestVm> 
{
    public AddExerciseValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty()
            .WithMessage("Exercise must be named");
    }
}