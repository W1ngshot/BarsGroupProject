using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class ArticleCategoryService : IArticleCategoryService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleCategoryService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> OrderByCategoryArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetOrderedArticlesAsync(category, period, count, firstIndex);
    }
}
