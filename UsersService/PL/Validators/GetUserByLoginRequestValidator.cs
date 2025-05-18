using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;

public class GetUserByLoginRequestValidator : AbstractValidator<GetUserByLoginRequest>
{
    public GetUserByLoginRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty()
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Current login must contain only latin letters and digits");
    }
}