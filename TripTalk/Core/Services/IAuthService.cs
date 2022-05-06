namespace Core.Services;

public interface IAuthService
{
    public Task<int> LoginAsync(string email, string password);

    public Task RegisterAsync(string nickname, string email, string password);
}