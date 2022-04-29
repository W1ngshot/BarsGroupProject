namespace Core.Services;

public interface ITagService
{
    public Task CreateTagAsync(string name);

    public Task<bool> IsTagExistsAsync(string name);
}
