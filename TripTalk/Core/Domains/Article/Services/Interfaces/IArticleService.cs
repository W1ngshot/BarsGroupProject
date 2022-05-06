namespace Core.Domains.Article.Services.Interfaces;

public interface IArticleService
{
    public Task<List<Article>> GetUserArticlesAsync(int userId, int count = int.MaxValue, int firstIndex = 0);

    public Task<Article> GetArticleByIdAsync(int articleId);

    public Task CreateArticleAsync(string title, string text, int userId, string? shortDescription = null, string? previewPictureLink = null, List<string>? tags = null);

    public Task EditArticleAsync(int articleId, string title, string text, string? shortDescription = null, string? previewPictureLink = null, List<string>? tags = null);

    public Task DeleteArticleAsync(int articleId);

    public Task<int> GetFilteredArticlesCountAsync(string searchLine, List<string>? tags);

    public Task<int> GetUserArticlesCountAsync(int userId);
}