namespace Core.Domains.Rate.Repository;

public interface IRateRepository
{
    public Task<int> GetRateAsync(int userId, int articleId);

    public Task SetRateAsync(int userId, int articleId, Rate rating);
}