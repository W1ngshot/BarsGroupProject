namespace Core.Domains.User.Repository;

public interface IAuthenticationRepository
{
    public Task<bool> IsNicknameOrEmailAreNotAvailableAsync(string nickname, string email);
}