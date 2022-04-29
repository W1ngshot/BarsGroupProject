using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class SearchService : ISearchService
{
    private readonly IArticleRepository _articleRepository;

    public SearchService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<List<Article>> FindArticlesAsync(string? searchLine = null, List<string>? tags = null, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetFilteredArticlesAsync(searchLine, tags, count, firstIndex);
    }
}
