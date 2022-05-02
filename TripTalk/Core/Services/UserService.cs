using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICryptographyService _cryptographyService;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ICryptographyService cryptographyService)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cryptographyService = cryptographyService;
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
    public async Task ChangePasswordAsync(string email, string oldPassword, string newPassword, string confirmPassword)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        var passwordHash = user.PasswordHash;
        var enteredPasswordHash = await _cryptographyService.EncryptPasswordAsync(oldPassword, user.PasswordSalt);

        if (passwordHash != enteredPasswordHash)
            throw new Exception(ErrorMessages.WrongPassword);

        user.PasswordHash = await _cryptographyService.EncryptPasswordAsync(newPassword, user.PasswordSalt);
        await _userRepository.UpdateUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}