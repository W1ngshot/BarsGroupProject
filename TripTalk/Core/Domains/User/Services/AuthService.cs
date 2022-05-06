using Core.Cryptography;
using Core.CustomExceptions.Messages;
using Core.Domains.User.Repository;
using Core.Domains.User.Services.Interfaces;
using FluentValidation;

namespace Core.Domains.User.Services;

public class AuthService : IAuthService
{
    #region Initialize Info
    private readonly IAuthenticationRepository _authenticationRepository;
    private readonly IUserRepository _userRepository;
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

    public async Task<int> LoginAsync(string nickname, string password)
    {
        var user = await _userRepository.GetUserByNicknameAsync(nickname);

        var enteredPasswordHash = await _cryptographyService.EncryptPasswordAsync(password, user.PasswordSalt);
        if (enteredPasswordHash != user.PasswordHash)
            throw new ValidationException(ErrorMessages.WrongPassword);
        return user.Id;
    }

    public async Task RegisterAsync(string nickname, string email, string password)
    {
        if (await _authenticationRepository.IsNicknameOrEmailAreNotAvailableAsync(nickname, email))
            throw new ValidationException(ValidationMessages.LoginOrEmailAlreadyExists);

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

        await _registerUserValidator.ValidateAndThrowAsync(user);

        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}