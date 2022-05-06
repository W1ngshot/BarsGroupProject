namespace Core.Domains.Article.Services.Interfaces;

public interface ISearchService
{
    public Task<List<Article>> FindArticlesAsync(string searchLine, List<string>? tags = null, int count = int.MaxValue, int firstIndex = 0);
}