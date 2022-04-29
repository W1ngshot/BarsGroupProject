namespace Core.Services;

public interface IRateService
{
    public Task<int> GetRate(int userId, int articleId);

    public Task SetRate(int userId, int articleId, int rating);
}