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

    public async Task<int> GetRateAsync(int userId, int articleId)
    {
        return await _rateRepository.GetRateAsync(userId, articleId);
    }

    public async Task SetRateAsync(int userId, int articleId, Rate rating)
    {
        await _rateRepository.SetRateAsync(userId, articleId, rating);
        await _unitOfWork.SaveChangesAsync();
    }
}
