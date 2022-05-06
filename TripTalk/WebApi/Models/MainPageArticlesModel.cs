using Core.Domains.Article;

namespace WebApi.Models;

public class MainPageArticlesModel
{
    public List<Article> PopularArticles { get; set; } = new();

    public List<Article> BestArticles { get; set; } = new();

    public List<Article> LatestArticles { get; set; } = new();
}