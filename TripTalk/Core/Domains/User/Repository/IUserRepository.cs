namespace Core.Domains.User.Repository;

public interface IUserRepository
{
    public Task AddUserAsync(User user);

    public Task UpdateUserAsync(User user);

    public Task<int> GetUserIdByNicknameAsync(string nickname);

    public Task<User> GetUserByNicknameAsync(string nickname);

    public Task<User> GetUserByIdAsync(int id);
}