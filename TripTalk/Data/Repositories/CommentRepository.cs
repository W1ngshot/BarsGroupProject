using Core.CustomExceptions;
using Core.CustomExceptions.Messages;
using Core.Domains.Comment;
using Core.Domains.Comment.Repository;
using Data.Db;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly TripTalkContext _context;

    public CommentRepository(TripTalkContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetArticleCommentsAsync(int articleId)
    {
        var commentModelList = await _context.Comments
            .AsNoTracking()
            .Include(comment => comment.User)
            .Where(comment => comment.ArticleId == articleId)
            .OrderByDescending(comment => comment.Date)
            .ToListAsync();

        return commentModelList.Select(entity => new Comment
        {
            Id = entity.Id,
            Message = entity.Message,
            Date = entity.Date,
            UserId = entity.UserId,
            ArticleId = entity.ArticleId,
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        }).ToList();
    }

    public async Task<Comment> GetCommentByIdAsync(int id)
    {
        var entity = await _context.Comments
                         .Include(comment => comment.User)
                         .FirstOrDefaultAsync(comment => comment.Id == id) ??
            throw new ValidationException(ErrorMessages.MissingComment);

        return new Comment
        {
            Id = entity.Id,
            Message = entity.Message,
            Date = entity.Date,
            UserId = entity.UserId,
            ArticleId = entity.ArticleId,
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        };
    }

    public async Task<int> AddCommentAsync(Comment comment)
    {
        var entity = new CommentDbModel
        {
            Message = comment.Message,
            Date = comment.Date,
            UserId = comment.UserId,
            ArticleId = comment.ArticleId
        };
        await _context.Comments.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity.Id;
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id) ??
            throw new ValidationException(ErrorMessages.MissingComment);

        entity.Message = comment.Message;
    }

    public async Task RemoveCommentAsync(int id)
    {
        var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id) ??
            throw new ValidationException(ErrorMessages.MissingComment);

        _context.Comments.Remove(entity);
    }
}
