using Core.Models;

namespace Core.RepositoryInterfaces;

public interface IUserRepository
{
    public Task AddUserAsync(User user);

    public Task UpdateUserAsync(User user);

    public Task<int> GetUserIdByEmail(string email);
}