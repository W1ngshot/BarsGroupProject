using Core.Domains.Rate;
using Core.Domains.Rate.Repository;
using Data.Db;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class RateRepository : IRateRepository
{
    private readonly TripTalkContext _context;

    public RateRepository(TripTalkContext context)
    {
        _context = context;
    }

    public async Task<int> GetRateAsync(int userId, int articleId)
    {
        var rate = await _context.Rates.FirstOrDefaultAsync(r => r.UserId == userId && r.ArticleId == articleId);

        return rate?.Rating ?? 0;
    }

    public async Task SetRateAsync(int userId, int articleId, Rate rating)
    {
        var rate = await _context.Rates.FirstOrDefaultAsync(r => r.UserId == userId && r.ArticleId == articleId);
        if (rate is null)
            await _context.AddAsync(new RateDbModel
            {
                UserId = userId,
                ArticleId = articleId,
                Rating = (int)rating
            });
        else
            rate.Rating = (int)rating;
    }
}
