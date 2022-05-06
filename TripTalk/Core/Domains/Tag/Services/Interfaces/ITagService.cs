namespace Core.Domains.Tag.Services.Interfaces;

public interface ITagService
{
    public Task AddTagsAsync(List<string> tags, int articleId);

    public Task<bool> IsTagExistsAsync(string name);
}
