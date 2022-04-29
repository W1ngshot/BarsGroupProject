using Core.RepositoryInterfaces;

namespace Core.Services;

public class RateService : IRateService
{
    private readonly IRateRepository _rateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RateService(IRateRepository rateRepository, IUnitOfWork unitOfWork)
    {
        _rateRepository = rateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> GetRate(int userId, int articleId)
    {
        return await _rateRepository.GetRate(userId, articleId);
    }

    public async Task SetRate(int userId, int articleId, int rating)
    {
        await _rateRepository.SetRate(userId, articleId, rating);
    }
}
