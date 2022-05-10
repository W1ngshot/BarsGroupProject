using Core.CustomExceptions.Messages;
using FluentValidation;

namespace Core.Domains.Tag.Validators;

public class TagsValidator : AbstractValidator<List<string>>
{
    public TagsValidator()
    {
        RuleFor(tags => tags)
            .Must(tags => tags.Count <= 3)
            .WithMessage(ValidationMessages.TooMuchTags);

        RuleForEach(tags => tags)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyTagName);
        RuleForEach(tags => tags)
            .MinimumLength(3)
            .WithMessage(ValidationMessages.TooShortTagName);
        RuleForEach(tags => tags)
            .MaximumLength(25)
            .WithMessage(ValidationMessages.TooLongTagName);
        RuleForEach(tags => tags)
            .Matches(@"^[\w\s,.!?\""':;`\-\\/(\)]+$")
            .WithMessage(ValidationMessages.TagNameContainsWrongSymbols);
    }
}