using Core.CustomExceptions.Messages;
using FluentValidation;

namespace Core.Domains.User.Validators;

public class SetPasswordValidator : AbstractValidator<SetPassword>
{
    public SetPasswordValidator()
    {
        RuleFor(form => form.Password)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyPassword);

        RuleFor(form => form.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyConfirmPassword);

        RuleFor(form => form.Password)
            .MinimumLength(8)
            .WithMessage(ValidationMessages.TooShortPassword);
        RuleFor(form => form.Password)
            .MaximumLength(40)
            .WithMessage(ValidationMessages.TooLongPassword);
        RuleFor(form => form.Password)
            .Equal(form => form.ConfirmPassword)
            .WithMessage(ValidationMessages.PasswordNotEqualToConfirmPassword);
        RuleFor(form => form.Password)
            .Matches(@"^[\w\s,.!?\""':;`\-\\/(\)]+$")
            .WithMessage(ValidationMessages.PasswordContainsWrongSymbols);
    }
}