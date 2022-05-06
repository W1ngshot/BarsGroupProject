namespace Core.Domains.Rate.Services.Interfaces;

public interface IRateService
{
    public Task<int> GetRateAsync(int userId, int articleId);

    public Task SetRateAsync(int userId, int articleId, Rate rating);
}