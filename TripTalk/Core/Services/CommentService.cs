using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Comment>> GetArticleCommentsAsync(int articleId)
    {
        return await _commentRepository.GetArticleCommentsAsync(articleId);
    }

    //TODO добавить валидацию
    public async Task CreateCommentAsync(string message, int userId, int articleId)
    {
        var comment = new Comment
        {
            Message = message,
            UserId = userId,
            Date = DateTime.UtcNow,
            ArticleId = articleId
        };

        await _commentRepository.CreateCommentAsync(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    //TODO добавить валидацию
    public async Task EditCommentAsync(int commentId, string message)
    {
        var comment = new Comment
        {
            Id = commentId,
            Message = message
        };

        await _commentRepository.UpdateCommentAsync(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(int commentId)
    {
        await _commentRepository.RemoveCommentAsync(commentId);
    }
}
