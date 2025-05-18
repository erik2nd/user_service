using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Login)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Login can only contain latin letters and digits")
            .NotEmpty();

        RuleFor(x => x.Password)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Password can only contain latin letters and digits")
            .NotEmpty();

        RuleFor(x => x.Name)
            .Matches("^[a-zA-Zа-яА-Я]+$")
            .WithMessage("Name can only contain latin or cyrillic letters")
            .NotEmpty();

        RuleFor(x => x.Gender)
            .InclusiveBetween(0, 2);
    }
}