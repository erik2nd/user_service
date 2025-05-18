using UsersService.PL.Models.Requests;
using FluentValidation;

namespace UsersService.PL.Validators;

public class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(x => x.Login)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Login can only contain latin letters and digits")
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Password can only contain latin letters and digits")
            .NotEmpty();
    }
}