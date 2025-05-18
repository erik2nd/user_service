using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;

public class UpdateUserProfileRequestValidator : AbstractValidator<UpdateUserProfileRequest>
{
    public UpdateUserProfileRequestValidator()
    {
        RuleFor(x => x.Login)
            .Matches("^[a-zA-Z0-9]+$")
            .WithMessage("Login can only contain latin letters and digits")
            .NotEmpty();

        When(x => x.NewName != null, () =>
        {
            RuleFor(x => x.NewName!)
                .Matches("^[a-zA-Zа-яА-ЯёЁ]+$")
                .WithMessage("Name must contain only latin or cyrillic letters");
        });

        When(x => x.NewGender.HasValue, () =>
        {
            RuleFor(x => x.NewGender!.Value)
                .InclusiveBetween(0, 2);
        });
    }
}
