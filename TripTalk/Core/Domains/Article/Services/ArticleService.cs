using Core.CustomExceptions.Messages;
using Core.Domains.Article.Repository;
using Core.Domains.Article.Services.Interfaces;
using Core.Domains.Tag.Services.Interfaces;
using FluentValidation;
using ValidationException = Core.CustomExceptions.ValidationException;

namespace Core.Domains.Article.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Article> _validator;
    private readonly ITagService _tagService;

    public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork,
        IValidator<Article> validator, ITagService tagService)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _tagService = tagService;
    }

    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetUserArticlesAsync(userId, count, firstIndex);
    }

    public async Task<Article> GetArticleByIdAsync(int articleId)
    {
        var article = await _articleRepository.GetArticleByIdAsync(articleId);

        article.Views++;
        await _articleRepository.UpdateArticleAsync(article);

        await _unitOfWork.SaveChangesAsync();
        return article;
    }

    public async Task<Article> CreateArticleAsync(string title, string text, int userId, string? shortDescription = null,
        string? previewPictureLink = null, List<string>? tags = null)
    {
        var article = new Article
        {
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            UploadDate = DateTime.UtcNow,
            UserId = userId,
            PreviewPictureLink = previewPictureLink,
            Views = 0
        };
        await _validator.ValidateAndThrowAsync(article);

        var articleId = await _articleRepository.AddArticleAsync(article);
        await _tagService.AddTagsAsync(tags ?? new List<string>(), articleId);
        
        return await _articleRepository.GetArticleByIdAsync(articleId);
    }

    public async Task<Article> EditArticleAsync(int articleId, string title, string text, string? shortDescription = null,
        string? previewPictureLink = null, List<string>? tags = null)
    {
        var article = new Article
        {
            Id = articleId,
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            PreviewPictureLink = previewPictureLink
        };
        await _validator.ValidateAndThrowAsync(article);

        await _articleRepository.UpdateArticleAsync(article);
        await _tagService.AddTagsAsync(tags ?? new List<string>(), articleId);

        return await _articleRepository.GetArticleByIdAsync(articleId);
    }

    public async Task DeleteArticleAsync(int articleId)
    {
        await _articleRepository.RemoveArticleAsync(articleId);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<int> GetFilteredArticlesCountAsync(string searchLine, List<string>? tags)
    {
        return await _articleRepository.GetFilteredArticlesCountAsync(searchLine, tags);
    }

    public async Task<int> GetUserArticlesCountAsync(int userId)
    {
        return await _articleRepository.GetUserArticlesCountAsync(userId);
    }

    public async Task EnsureArticleAuthorshipAsync(int userId, int articleId)
    {
        var article = await GetArticleByIdAsync(userId);
        if (article.UserId != userId)
            throw new ValidationException(ErrorMessages.SomeoneElseArticle);
    }
}