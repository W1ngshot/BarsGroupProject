using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IAuthenticationRepository
{
    public Task<bool> EnsureNicknameOrEmailAreAvailableAsync(string nickname, string email);
}