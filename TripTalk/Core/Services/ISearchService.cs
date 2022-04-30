using Core.Models;

namespace Core.Services;

public interface ISearchService
{
    public Task<List<Article>> FindArticlesAsync(string searchLine, List<string>? tags = null, int count = int.MaxValue, int firstIndex = 0);
}