using Core.Models;

namespace Core.Services;

public interface IArticleService
{
    public enum Period
    {
        Today,
        LastWeek,
        LastMonth,
        AllTime
    }

    public enum Category
    {
        Popular,
        Last,
        Best
    }

    public Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int first, int count);

    public Task<List<Article>> GetUserArticlesAsync(int userId, int first, int count);

    public Task<Article> GetArticleByIdAsync(int articleId);

    public Task CreateArticleAsync(string title, string shortDescription, string text, string? pictureLink, int userId);

    public Task EditArticleAsync(int articleId, string title, string shortDescription, string text, string? pictureLink);
}