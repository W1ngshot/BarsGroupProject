namespace Core.RepositoryInterfaces;

public interface IRateRepository
{
    public Task<int> GetRate(int userId, int articleId);

    public Task SetRate(int userId, int articleId, int rating);
}