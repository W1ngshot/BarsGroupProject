using Core.RepositoryInterfaces;
using Core.Services;
using Data.Db;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly TripTalkContext _context;
    private readonly ICryptographyService _cryptographyService;

    public AuthenticationRepository(TripTalkContext context, ICryptographyService cryptographyService)
    {
        _context = context;
        _cryptographyService = cryptographyService;
    }

    public async Task<bool> EnsureNicknameOrEmailAreAvailableAsync(string nickname, string email) =>
        await _context.Users.AnyAsync(user => user.Nickname == nickname || user.Email == email);

    public async Task<bool> EnsureUserDataValidAsync(string email, string password)
    {
        return await _context.Users.AnyAsync(user =>
        user.Email == email &&
        user.PasswordHash == _cryptographyService.EncryptPasswordAsync(password, user.PasswordSalt).Result);
    }
}