namespace Core.Domains.Tag.Repository;

public interface ITagRepository
{
    public Task AddTagAsync(string name);

    public Task AttachTagsAsync(List<string> tags, int articleId);

    public Task<bool> IsTagExistsAsync(string name);
}
