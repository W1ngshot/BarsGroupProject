namespace Core.RepositoryInterfaces;

public interface IAuthenticationRepository
{
    public Task<bool> EnsureNicknameOrEmailAreAvailableAsync(string nickname, string email);

    public Task<bool> EnsureUserDataValidAsync(string email, string password);
}