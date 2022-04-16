namespace Core.Services;

public interface IUserService
{
    public Task<int> GetUserIdByEmail(string email);
}