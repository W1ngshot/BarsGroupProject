using Core.Models;

namespace Core.Services;

public interface IUserService
{
    public Task<User> GetUserByIdAsync(int id);

    public Task<int> GetUserIdByNicknameAsync(string nickname);

    public Task<User> GetUserByNicknameAsync(string nickname);

    public Task ChangePasswordAsync(string nickname, string oldPassword, string newPassword, string confirmPassword);
}