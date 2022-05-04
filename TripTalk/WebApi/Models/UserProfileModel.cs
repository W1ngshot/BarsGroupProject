using Core.Models;

namespace WebApi.Models;

public class UserProfileModel
{
    public PublicUserModel User { get; set; }
    public List<Article> Articles { get; set; } = new();
}