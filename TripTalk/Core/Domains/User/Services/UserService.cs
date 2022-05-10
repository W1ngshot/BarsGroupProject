using Core.Cryptography;
using Core.CustomExceptions.Messages;
using Core.Domains.User.Repository;
using Core.Domains.User.Services.Interfaces;
using FluentValidation;
using ValidationException = Core.CustomExceptions.ValidationException;

namespace Core.Domains.User.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICryptographyService _cryptographyService;
    private readonly IValidator<ChangePassword> _validator;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork,
        ICryptographyService cryptographyService, IValidator<ChangePassword> validator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _cryptographyService = cryptographyService;
        _validator = validator;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }

    public async Task<int> GetUserIdByNicknameAsync(string nickname)
    {
        return await _userRepository.GetUserIdByNicknameAsync(nickname);
    }

    public async Task<User> GetUserByNicknameAsync(string nickname)
    {
        return await _userRepository.GetUserByNicknameAsync(nickname);
    }

    public async Task ChangePasswordAsync(string nickname, string oldPassword, string newPassword, string confirmPassword)
    {
        await _validator.ValidateAndThrowAsync(new ChangePassword
        {
            Password = newPassword,
            ConfirmPassword = confirmPassword
        });

        var user = await _userRepository.GetUserByNicknameAsync(nickname);

        var passwordHash = user.PasswordHash;
        var enteredPasswordHash = await _cryptographyService.EncryptPasswordAsync(oldPassword, user.PasswordSalt);

        if (passwordHash != enteredPasswordHash)
            throw new ValidationException(ErrorMessages.WrongPassword);

        user.PasswordHash = await _cryptographyService.EncryptPasswordAsync(newPassword, user.PasswordSalt);
        await _userRepository.UpdateUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}