using Core.Models;

namespace Core.Services;

public interface IArticleService
{
    public Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0);

    public Task<List<Article>> GetUserArticlesAsync(int userId, int count = int.MaxValue, int firstIndex = 0);

    public Task<Article> GetArticleByIdAsync(int articleId);

    public Task CreateArticleAsync(string title, string text, int userId, string? shortDescription = null, string? previewPictureLink = null, List<string>? attachedPicturesLinks = null);

    public Task EditArticleAsync(int articleId, string title, string text, string? shortDescription = null, string? previewPictureLink = null, List<string>? attachedPicturesLinks = null);
}