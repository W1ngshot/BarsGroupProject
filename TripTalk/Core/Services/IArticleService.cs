using Core.Models;

namespace Core.Services;

public interface IArticleService
{
    public Task<Article> GetArticleByIdAsync(int articleId);

    public Task CreateArticleAsync(string title, string? shortDescription, string text, string? previewPictureLink, List<string>? attachedPicturesLinks, int userId);

    public Task EditArticleAsync(int articleId, string title, string? shortDescription, string text, string? previewPictureLink, List<string>? attachedPicturesLinks);

    public Task<List<Article>> GetUserArticles(int userId);
}