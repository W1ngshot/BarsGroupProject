using Core.Models;

namespace WebApi.Models;

public class SearchModel
{
    public List<Article> SearchedArticles { get; set; } = new();
}