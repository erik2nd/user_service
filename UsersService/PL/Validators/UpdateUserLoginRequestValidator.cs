using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;

using FluentValidation;

public class UpdateUserLoginRequestValidator : AbstractValidator<UpdateUserLoginRequest>
{
    public UpdateUserLoginRequestValidator()
    {
        RuleFor(x => x.CurrentLogin)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Current login must contain only latin letters and digits");

        RuleFor(x => x.NewLogin)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("New login must contain only latin letters and digits");
    }
}