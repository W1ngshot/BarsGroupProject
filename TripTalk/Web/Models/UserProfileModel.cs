using Core.Models;

namespace OldWeb.Models;

public class UserProfileModel
{
    public User User { get; set; }
    public List<Article> Articles { get; set; }
}