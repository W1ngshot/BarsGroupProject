using Core.RepositoryInterfaces;
using Core.Models;
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

    public async Task<User> GetUserByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);
        if (entity == null)
            throw new Exception("Данный email не зарегистрирован");

        return new User
        {
            Nickname = entity.Nickname,
            Email = entity.Email,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt,
            AvatarLink = entity.AvatarLink,
            RegistrationDate = entity.RegistrationDate
        };
    }
}