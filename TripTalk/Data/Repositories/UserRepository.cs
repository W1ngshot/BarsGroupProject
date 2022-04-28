﻿using Core.Models;
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
                     throw new Exception("Данного пользователя не существует");
        //TODO разобраться с exception
        entity.Nickname = user.Nickname;
        entity.Email = user.Email;
        entity.PasswordHash = user.PasswordHash;
        entity.AvatarLink = user.AvatarLink;
    }

    public async Task<int> GetUserIdByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ??
                     throw new Exception("Данного пользователя не существует");
        return entity.Id;
    }

    //TODO переделать Core.Models в Domains, а Models возможно перенести из Web в Core, чтобы не передавать в данном случае пароль
    public async Task<User> GetUserByEmailAsync(string email)
    {
        var entity = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ??
                     throw new Exception("Данного пользователя не существует");
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