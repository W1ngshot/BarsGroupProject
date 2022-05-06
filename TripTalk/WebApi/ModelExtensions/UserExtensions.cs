using Core.Domains.User;
using WebApi.Models;

namespace WebApi.ModelExtensions;

public static class UserExtensions
{
    public static PublicUserModel ToPublicUser(this User user) => 
        new()
        {
            Id = user.Id,
            Nickname = user.Nickname,
            Email = user.Email,
            AvatarLink = user.AvatarLink,
            RegistrationDate = user.RegistrationDate
        };
}