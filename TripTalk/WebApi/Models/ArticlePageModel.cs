using Core.Domains.Article;
using Core.Domains.Comment;

namespace WebApi.Models;

public class ArticlePageModel
{
    public Article Article { get; set; }
    public List<Comment> Comments { get; set; } = new();
}