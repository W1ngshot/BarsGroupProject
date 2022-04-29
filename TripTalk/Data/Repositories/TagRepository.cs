using Core.RepositoryInterfaces;
using Data.Db;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class TagRepository : ITagRepository
{
    private readonly TripTalkContext _context;

    public TagRepository(TripTalkContext context)
    {
        _context = context;
    }
    public async Task AddTagAsync(string name)
    {
        var entity = new TagDbModel
        {
            Name = name
        };

        await _context.Tags.AddAsync(entity);
    }

    public async Task<bool> IsTagExistsAsync(string name)
    {
        return await _context.Tags.AnyAsync(tag => tag.Name == name);
    }
}
