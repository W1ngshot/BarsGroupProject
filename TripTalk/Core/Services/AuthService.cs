using Core.Models;
using Core.RepositoryInterfaces;
using FluentValidation;

namespace Core.Services;

public class AuthService : IAuthService
{
    #region Initialize Info
    private readonly IAuthenticationRepository _authenticationRepository; //TODO
    private readonly IUserRepository _userRepository; //TODO
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<User> _registerUserValidator;
    private readonly ICryptographyService _cryptographyService;

    public AuthService(IAuthenticationRepository authenticationRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IValidator<User> registerUserValidator,
        ICryptographyService cryptographyService)
    {
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _registerUserValidator = registerUserValidator;
        _cryptographyService = cryptographyService;
    }
    #endregion

    //TODO возможно подумать над названием
    public async Task LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        var enteredPasswordHash = await _cryptographyService.EncryptPasswordAsync(password, user.PasswordSalt);
        if (enteredPasswordHash != user.PasswordHash)
            throw new Exception("Пароль введён неверно");
    }

    //TODO возможно подумать над названием
    public async Task RegisterAsync(string nickname, string email, string password)
    {
        if (await _authenticationRepository.IsNicknameOrEmailAreNotAvailableAsync(nickname, email))
            throw new Exception(ValidationMessages.LoginOrEmailAlreadyExists); //TODO придумать что-то сюда

        var passwordSalt = _cryptographyService.GenerateSalt();
        var passwordHash = await _cryptographyService.EncryptPasswordAsync(password, passwordSalt);

        var user = new User
        {
            Nickname = nickname,
            Email = email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RegistrationDate = DateTime.UtcNow
        };

        await _registerUserValidator.ValidateAndThrowAsync(user); //TODO разобраться с exception

        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}