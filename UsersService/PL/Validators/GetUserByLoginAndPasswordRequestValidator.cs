using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;


public class GetUserByLoginAndPasswordRequestValidator : AbstractValidator<GetUserByLoginAndPasswordRequest>
{
    public GetUserByLoginAndPasswordRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Login must contain only latin letters and digits");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("Password must contain only latin letters and digits");
    }
}
