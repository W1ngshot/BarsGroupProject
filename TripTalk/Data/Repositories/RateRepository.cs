using Core.RepositoryInterfaces;
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

    public async Task<int> GetRate(int userId, int articleId)
    {
        var rate = await _context.Rates.FirstOrDefaultAsync(r => r.UserId == userId && r.ArticleId == articleId);

        return rate?.Rating ?? 0;
    }

    public async Task SetRate(int userId, int articleId, int rating)
    {
        var rate = await _context.Rates.FirstOrDefaultAsync(r => r.UserId == userId && r.ArticleId == articleId);
        if (rate is null)
            await _context.AddAsync(new RateDbModel
            {
                UserId = userId,
                ArticleId = articleId,
                Rating = rating
            });
        else
            rate.Rating = rating;
    }
}
