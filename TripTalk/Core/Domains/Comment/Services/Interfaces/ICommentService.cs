﻿namespace Core.Domains.Comment.Services.Interfaces;

public interface ICommentService
{
    public Task<List<Comment>> GetArticleCommentsAsync(int articleId);

    public Task<Comment> GetCommentByIdAsync(int commentId);

    public Task CreateCommentAsync(string message, int userId, int articleId);

    public Task EditCommentAsync(int commentId, string message);

    public Task DeleteCommentAsync(int commentId);

    public Task EnsureCommentAuthorshipAsync(int userId, int commentId);
}
