using Core.RepositoryInterfaces;
using Data.Db;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class AuthenticationRepository : IAuthenticationRepository
{
    private readonly TripTalkContext _context;

    public AuthenticationRepository(TripTalkContext context)
    {
        _context = context;
    }

    public async Task<bool> EnsureNicknameOrEmailAreAvailableAsync(string nickname, string email) => 
        await _context.Users.AnyAsync(user => user.Nickname == nickname || user.Email == email);

    public async Task<bool> EnsureUserDataValidAsync(string email, string password) =>
        await _context.Users.AnyAsync(user => user.Email == email && user.Password == password);
}