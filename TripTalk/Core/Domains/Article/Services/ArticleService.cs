using Core.Domains.Article.Repository;
using Core.Domains.Article.Services.Interfaces;
using FluentValidation;

namespace Core.Domains.Article.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<Article> _validator;

    public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork, IValidator<Article> validator)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetUserArticlesAsync(userId, count, firstIndex);
    }

    public async Task<Article> GetArticleByIdAsync(int articleId)
    {
        var article = await _articleRepository.GetArticleByIdAsync(articleId);
        await _unitOfWork.SaveChangesAsync();
        return article;
    }

    public async Task CreateArticleAsync(string title, string text, int userId, string? shortDescription = null,
        string? previewPictureLink = null, List<string>? attachedPicturesLinks = null)
    {
        var article = new Article
        {
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            UploadDate = DateTime.UtcNow,
            UserId = userId,
            PreviewPictureLink = previewPictureLink,
            Rating = 0,
            Views = 0
        };
        await _validator.ValidateAndThrowAsync(article);

        await _articleRepository.AddArticleAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task EditArticleAsync(int articleId, string title, string text, string? shortDescription = null,
        string? previewPictureLink = null, List<string>? attachedPicturesLinks = null)
    {
        var article = new Article
        {
            Id = articleId,
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            PreviewPictureLink = previewPictureLink,
        };
        await _validator.ValidateAndThrowAsync(article);

        await _articleRepository.UpdateArticleAsync(article);
        await _unitOfWork.SaveChangesAsync();
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
}