using Core.CustomExceptions.Messages;
using FluentValidation;

namespace Core.Domains.Comment.Validators;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(comment => comment.Message)
            .NotEmpty()
            .WithMessage(ValidationMessages.EmptyCommentMessage);
        RuleFor(comment => comment.Message)
            .MaximumLength(250)
            .WithMessage(ValidationMessages.TooLongCommentMessage);
        RuleFor(comment => comment.Message)
            .Matches(@"^[\w\s,.!?\""':;`\-\\/(\)]+$")
            .WithMessage(ValidationMessages.CommentMessageContainsWrongSymbols);
    }
}