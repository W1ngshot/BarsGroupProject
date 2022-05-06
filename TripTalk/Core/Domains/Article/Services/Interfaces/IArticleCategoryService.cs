namespace Core.Domains.Article.Services.Interfaces;

public interface IArticleCategoryService
{
    public Task<List<Article>> GetOrderedArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0);

    public Task<int> GetArticlesCount();
}