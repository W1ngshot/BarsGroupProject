using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class AuthService : IAuthService
{
    private readonly IAuthenticationRepository _authenticationRepository; //TODO
    private readonly IUserRepository _userRepository; //TODO
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(IAuthenticationRepository authenticationRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _authenticationRepository = authenticationRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    //TODO возможно подумать над названием
    public async Task LoginAsync(string email, string password)
    {
        //TODO шифровать пароль
        if (!await _authenticationRepository.EnsureUserDataValidAsync(email, password))
            throw new Exception("Почта или пароль введены неправильно");
    }

    //TODO возможно подумать над названием
    public async Task RegisterAsync(string nickname, string email, string password)
    {
        if (!await _authenticationRepository.EnsureNicknameOrEmailAreAvailableAsync(nickname, email))
            throw new Exception("Логин или никнейм заняты"); //TODO придумать что-то сюда
        //TODO добавить шифрование пароля
        var user = new User
        {
            Email = email,
            Nickname = nickname,
            Password = password,
            RegistrationDate = DateTime.UtcNow
        };
        //TODO добавить валидатор
        await _userRepository.AddUserAsync(user);
        await _unitOfWork.SaveChangesAsync();
    }
}