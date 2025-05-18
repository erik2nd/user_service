using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;


public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Login can only contain latin letters and digits")
            .NotEmpty();
    }
}
