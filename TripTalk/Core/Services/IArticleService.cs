using Core.Models;

namespace Core.Services;

public interface IArticleService
{
    public Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int count = 0, int firstIndex = 0);

    public Task<List<Article>> GetUserArticlesAsync(int userId, int count = 0, int firstIndex = 0);

    public Task<Article> GetArticleByIdAsync(int articleId);

    public Task CreateArticleAsync(string title, string shortDescription, string text, string? pictureLink, int userId);

    public Task EditArticleAsync(int articleId, string title, string shortDescription, string text, string? pictureLink);
}