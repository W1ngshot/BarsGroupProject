using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetUserIdByEmailAsync(string email)
    {
        return await _userRepository.GetUserIdByEmailAsync(email);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetUserByEmailAsync(email);
    }

    //TODO добавить валидацию введенных паролей
    //TODO добавить шифрование пароля
    public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user.Password != oldPassword)
            throw new Exception("Неправильный пароль");

        user.Password = newPassword;
        await _userRepository.UpdateUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}