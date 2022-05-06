namespace Core.Domains.User.Services.Interfaces;

public interface IUserService
{
    public Task<User> GetUserByIdAsync(int id);

    public Task<int> GetUserIdByEmailAsync(string email);

    public Task<User> GetUserByEmailAsync(string email);

    public Task ChangePasswordAsync(string email, string oldPassword, string newPassword, string confirmPassword);
}