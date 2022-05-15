using Core.CustomExceptions;
using Core.CustomExceptions.Messages;
using Core.Domains.User;
using Core.Domains.User.Repository;
using Data.Db;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly TripTalkContext _context;

    public UserRepository(TripTalkContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        var entity = new UserDbModel
        {
            Nickname = user.Nickname,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            PasswordSalt = user.PasswordSalt,
            AvatarLink = user.AvatarLink,
            RegistrationDate = user.RegistrationDate
        };

        await _context.Users.AddAsync(entity);
    }

    public async Task UpdateUserAsync(User user)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) ??
            throw new NotFoundException();

        entity.Nickname = user.Nickname;
        entity.Email = user.Email;
        entity.PasswordHash = user.PasswordHash;
        entity.AvatarLink = user.AvatarLink;
    }

    public async Task<int> GetUserIdByNicknameAsync(string nickname)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Nickname == nickname) ??
            throw new NotFoundException();
        return entity.Id;
    }

    public async Task<User> GetUserByNicknameAsync(string nickname)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Nickname == nickname) ??
            throw new NotFoundException();
        return new User
        {
            Id = entity.Id,
            Email = entity.Email,
            Nickname = entity.Nickname,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt,
            AvatarLink = entity.AvatarLink,
            RegistrationDate = entity.RegistrationDate,
        };
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(user => user.Id == id) ??
                     throw new NotFoundException();
        return new User
        {
            Id = entity.Id,
            Email = entity.Email,
            Nickname = entity.Nickname,
            PasswordHash = entity.PasswordHash,
            PasswordSalt = entity.PasswordSalt,
            AvatarLink = entity.AvatarLink,
            RegistrationDate = entity.RegistrationDate,
        };
    }
}