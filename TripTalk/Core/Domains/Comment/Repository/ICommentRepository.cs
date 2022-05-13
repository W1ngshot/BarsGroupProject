namespace Core.Domains.Comment.Repository;

public interface ICommentRepository
{
    public Task<List<Comment>> GetArticleCommentsAsync(int articleId);

    public Task<Comment> GetCommentByIdAsync(int id);

    public Task AddCommentAsync(Comment comment);

    public Task UpdateCommentAsync(Comment comment);

    public Task RemoveCommentAsync(int id);
}