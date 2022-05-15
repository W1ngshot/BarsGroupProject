using Core.Domains.Article;

namespace WebApi.Models;

public class UserProfileModel
{
    public PublicUserModel User { get; set; }
    public List<Article> Articles { get; set; } = new();
    public int TotalCount { get; set; } = 0;
}