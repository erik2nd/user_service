using FluentValidation;
using UsersService.PL.Models.Requests;

namespace UsersService.PL.Validators;


public class GetUsersByAgeRequestValidator : AbstractValidator<UsersByAgeRequest>
{
    public GetUsersByAgeRequestValidator()
    {
        RuleFor(x => x.Age)
            .GreaterThanOrEqualTo(0).WithMessage("Age must be equal or greater than zero");
    }
}
