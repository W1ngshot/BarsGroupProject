using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IArticleRepository
{
    public Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int count, int firstIndex);

    public Task<List<Article>> GetUserArticlesAsync(int userId, int count, int firstIndex);

    public Task<Article> GetArticleByIdAsync(int id);

    public Task CreateArticleAsync(Article article);

    public Task UpdateArticleAsync(Article article);
}