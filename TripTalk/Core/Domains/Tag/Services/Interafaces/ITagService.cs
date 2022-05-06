namespace Core.Domains.Tag.Services.Interafaces;

public interface ITagService
{
    public Task CreateTagAsync(string name);

    public Task<bool> IsTagExistsAsync(string name);
}
