using Core.Domains.Tag.Repository;
using Core.Domains.Tag.Services.Interfaces;

namespace Core.Domains.Tag.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task AddTagsAsync(List<string> tags, int articleId)
    {
        //TODO доделать метод, поменяв реализацию tagRepository
        //await _tagRepository.AddTagAsync(name);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsTagExistsAsync(string name)
    {
        return await _tagRepository.IsTagExistsAsync(name);
    }
}
