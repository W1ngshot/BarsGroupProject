using Core.Models;

namespace Core.Services;

public interface IArticleCategoryService
{
    public Task<List<Article>> OrderByCategoryArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0);
}