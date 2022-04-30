using Core.Models;

namespace Web.Models;

public class MainPageArticlesModel
{
    public List<Article> PopularArticles { get; set; }

    public List<Article> BestArticles { get; set; }

    public List<Article> LatestArticles { get; set; }
}