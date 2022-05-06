namespace Core.Domains.User.Services.Interfaces;

public interface IAuthService
{
    public Task LoginAsync(string email, string password);

    public Task RegisterAsync(string nickname, string email, string password);
}