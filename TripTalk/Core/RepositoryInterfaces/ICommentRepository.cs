using Core.Models;

namespace Core.RepositoryInterfaces;

public interface ICommentRepository
{
    public Task<List<Comment>> GetArticleCommentsAsync(int articleId);

    public Task AddCommentAsync(Comment comment);

    public Task UpdateCommentAsync(Comment comment);

    public Task RemoveCommentAsync(int id);
}