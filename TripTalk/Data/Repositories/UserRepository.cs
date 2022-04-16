using Core.Models;
using Core.RepositoryInterfaces;
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
            Password = user.Password,
            RegistrationDate = user.RegistrationDate,
            AvatarLink = user.AvatarLink
        };

        await _context.Users.AddAsync(entity);
    }

    public async Task UpdateUserAsync(User user)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id) ??
                     throw new Exception("Данного пользователя не существует");
        //TODO разобраться с exception
        entity.Nickname = user.Nickname;
        entity.Email = user.Email;
        entity.Password = user.Password;
        entity.AvatarLink = user.AvatarLink;
    }

    public async Task<int> GetUserIdByEmail(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ??
                     throw new Exception("Данного пользователя не существует");
        return entity.Id;
    }
}