using Core.CustomExceptions.Messages;
using FluentValidation;

namespace Core.Domains.User.Validators;

public class ChangePasswordValidator : AbstractValidator<ChangePassword>
{
    public ChangePasswordValidator()
    {
        RuleFor(form => form.Password)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyNewPassword);

        RuleFor(form => form.ConfirmPassword)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyConfirmPassword);

        RuleFor(form => form.Password)
            .MinimumLength(8)
            .WithMessage(ValidationMessages.TooShortNewPassword);
        RuleFor(form => form.Password)
            .MaximumLength(40)
            .WithMessage(ValidationMessages.TooLongNewPassword);
        RuleFor(form => form.Password)
            .Equal(form => form.ConfirmPassword)
            .WithMessage(ValidationMessages.NewPasswordNotEqualToConfirmPassword);
        RuleFor(form => form.Password)
            .Matches(@"^[\w\s,.!?\""':;`\-\\/(\)]+$")
            .WithMessage(ValidationMessages.NewPasswordContainsWrongSymbols);
    }
}