using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IAuthenticationRepository
{
    public Task<bool> IsNicknameOrEmailAreNotAvailableAsync(string nickname, string email);
}