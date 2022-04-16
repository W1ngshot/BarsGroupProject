using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IArticleRepository
{
    public Task<Article> GetArticleByIdAsync(int id);

    public Task CreateArticleAsync(Article article);

    public Task UpdateArticleAsync(Article article);

    public Task<List<Article>> GetUserArticlesAsync(int userId);
}