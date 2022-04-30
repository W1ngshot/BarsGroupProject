using Core;
using Core.Models;
using Core.RepositoryInterfaces;
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
            .Where(comment => comment.ArticleId == articleId)
            .ToListAsync();

        return commentModelList.Select(entity => new Comment
        {
            Id = entity.Id,
            Message = entity.Message,
            Date = entity.Date,
            UserId = entity.UserId,
            ArticleId = entity.ArticleId
        }).ToList();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        var entity = new CommentDbModel
        {
            Id = comment.Id,
            Message = comment.Message,
            Date = comment.Date,
            UserId = comment.UserId,
            ArticleId = comment.ArticleId
        };
        await _context.Comments.AddAsync(entity);
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id) ??
            throw new Exception(ErrorMessages.MissingComment);

        entity.Message = comment.Message;
    }

    public async Task RemoveCommentAsync(int id)
    {
        var entity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id) ??
            throw new Exception(ErrorMessages.MissingComment);

        _context.Comments.Remove(entity);
    }
}
