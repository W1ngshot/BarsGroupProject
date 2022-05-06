using Core.Domains.Article.Repository;
using Core.Domains.Article.Services.Interfaces;

namespace Core.Domains.Article.Services;

public class SearchService : ISearchService
{
    private readonly IArticleRepository _articleRepository;

    public SearchService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> FindArticlesAsync(string searchLine, List<string>? tags = null, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetFilteredArticlesAsync(searchLine, tags, count, firstIndex);
    }
}
