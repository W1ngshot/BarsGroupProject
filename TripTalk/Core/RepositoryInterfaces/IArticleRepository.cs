using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IArticleRepository
{
    public Task<Article> GetArticleById(int id);

    public Task CreateArticle(Article article);

    public Task UpdateArticle(Article article);
}