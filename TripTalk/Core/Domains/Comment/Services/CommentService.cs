using Core.CustomExceptions.Messages;
using Core.Domains.Comment.Repository;
using Core.Domains.Comment.Services.Interfaces;
using FluentValidation;
using ValidationException = Core.CustomExceptions.ValidationException;

namespace Core.Domains.Comment.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Comment> _validator;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork, IValidator<Comment> validator)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<List<Comment>> GetArticleCommentsAsync(int articleId)
    {
        return await _commentRepository.GetArticleCommentsAsync(articleId);
    }

    public async Task<Comment> GetCommentByIdAsync(int commentId)
    {
        return await _commentRepository.GetCommentByIdAsync(commentId);
    }

    public async Task CreateCommentAsync(string message, int userId, int articleId)
    {
        var comment = new Comment
        {
            Message = message,
            UserId = userId,
            Date = DateTime.UtcNow,
            ArticleId = articleId
        };

        await _validator.ValidateAndThrowAsync(comment);

        await _commentRepository.AddCommentAsync(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EditCommentAsync(int commentId, string message)
    {
        var comment = new Comment
        {
            Id = commentId,
            Message = message
        };

        await _validator.ValidateAndThrowAsync(comment);

        await _commentRepository.UpdateCommentAsync(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        await _commentRepository.RemoveCommentAsync(commentId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EnsureCommentAuthorshipAsync(int userId, int commentId)
    {
        var article = await GetCommentByIdAsync(commentId);
        if (article.UserId != userId)
            throw new ValidationException(ErrorMessages.SomeoneElseComment);
    }
}
