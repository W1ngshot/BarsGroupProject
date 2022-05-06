using Core.Domains.Article.Repository;
using Core.Domains.Article.Services.Interfaces;

namespace Core.Domains.Article.Services;

public class ArticleCategoryService : IArticleCategoryService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleCategoryService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> GetOrderedArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetOrderedArticlesAsync(category, period, count, firstIndex);
    }

    public async Task<int> GetArticlesCount()
    {
        return await _articleRepository.GetArticlesCountAsync();
    }
}
