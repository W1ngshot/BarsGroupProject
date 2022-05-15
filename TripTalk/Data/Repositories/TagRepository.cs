using Core.CustomExceptions;
using Core.CustomExceptions.Messages;
using Core.Domains.Tag.Repository;
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

    public async Task AttachTagsAsync(List<string> tags, int articleId)
    {
        var articleDb = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == articleId) ??
            throw new NotFoundException();
        var tagsDb = await _context.Tags.Where(t => tags.Contains(t.Name)).ToListAsync();

        foreach (var tag in articleDb.Tags)
            tag.Articles.Remove(articleDb);
        foreach (var tag in tagsDb)
            tag.Articles.Add(articleDb);

        articleDb.Tags = tagsDb;
    }

    public async Task<bool> IsTagExistsAsync(string name)
    {
        return await _context.Tags.AnyAsync(tag => tag.Name == name);
    }
}
