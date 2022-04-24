using Core.Models;
using Core.Services;

namespace Core.RepositoryInterfaces;

public interface IArticleRepository
{
    public Task<List<Article>> GetCategoryArticlesAsync(IArticleService.Category category, IArticleService.Period period, int first, int count);

    public Task<List<Article>> GetUserArticlesAsync(int userId, int first, int count);

    public Task<Article> GetArticleByIdAsync(int id);

    public Task CreateArticleAsync(Article article);

    public Task UpdateArticleAsync(Article article);
}