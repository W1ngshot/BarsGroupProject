using Core.RepositoryInterfaces;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<int> GetUserIdByEmail(string email)
    {
        return await _userRepository.GetUserIdByEmail(email);
    }
}