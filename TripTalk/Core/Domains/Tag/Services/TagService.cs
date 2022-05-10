using Core.Domains.Tag.Repository;
using Core.Domains.Tag.Services.Interfaces;
using FluentValidation;

namespace Core.Domains.Tag.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<List<string>> _validator;

    public TagService(ITagRepository tagRepository, IUnitOfWork unitOfWork, IValidator<List<string>> validator)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task AddTagsAsync(List<string> tags, int articleId)
    {
        await _validator.ValidateAndThrowAsync(tags);

        foreach (var tag in tags)
            if (!await IsTagExistsAsync(tag))
                await _tagRepository.AddTagAsync(tag);
        await _unitOfWork.SaveChangesAsync();

        await _tagRepository.AttachTagsAsync(tags, articleId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> IsTagExistsAsync(string name)
    {
        return await _tagRepository.IsTagExistsAsync(name);
    }
}
