using Core.CustomExceptions.Messages;
using FluentValidation;

namespace Core.Domains.User.Validators;

public class RegisterUserValidator : AbstractValidator<User>
{
    public RegisterUserValidator()
    {
        RuleFor(user => user.Nickname)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyLogin);
        RuleFor(user => user.Nickname)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortLogin);
        RuleFor(user => user.Nickname)
            .MaximumLength(25)
            .WithMessage(ValidationMessages.TooLongLogin);
        RuleFor(user => user.Nickname)
            .Matches(@"^[\w\s]+$")
            .WithMessage(ValidationMessages.LoginContainsWrongSymbols);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyEmail);
        RuleFor(user => user.Email)
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage(ValidationMessages.IncorrectEmail);
    }
}
