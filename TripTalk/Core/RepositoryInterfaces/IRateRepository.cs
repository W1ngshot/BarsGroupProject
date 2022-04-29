namespace Core.RepositoryInterfaces;

public interface IRateRepository
{
    public Task<int> GetRateAsync(int userId, int articleId);

    public Task SetRateAsync(int userId, int articleId, int rating);
}