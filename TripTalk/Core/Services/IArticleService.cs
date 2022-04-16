using Core.Models;

namespace Core.Services;

public interface IArticleService
{
    public Task<Article> GetArticleById(int articleId);

    public Task CreateArticle(string title, string shortDescription, string text, string? pictureLink, int userId);

    public Task EditArticle(int articleId, string title, string shortDescription, string text, string? pictureLink);
}