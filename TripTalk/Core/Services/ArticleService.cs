using Core.Models;
using Core.RepositoryInterfaces;

namespace Core.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ArticleService(IArticleRepository articleRepository, IUnitOfWork unitOfWork)
    {
        _articleRepository = articleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetCategoryArticlesAsync(category, period, count, firstIndex);
    }

    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count = int.MaxValue, int firstIndex = 0)
    {
        return await _articleRepository.GetUserArticlesAsync(userId, count, firstIndex);
    }

    public async Task<Article> GetArticleByIdAsync(int articleId)
    {
        return await _articleRepository.GetArticleByIdAsync(articleId);
    }

    //TODO добавить валидацию
    public async Task CreateArticleAsync(string title, string text, int userId, string? shortDescription = null, string? previewPictureLink = null, List<string>? attachedPicturesLinks = null)
    {
        var article = new Article
        {
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            UploadDate = DateTime.UtcNow,
            UserId = userId,
            PreviewPictureLink = previewPictureLink,
            AttachedPicturesLinks = attachedPicturesLinks,
            Rating = 0,
            Views = 0
        };

        await _articleRepository.CreateArticleAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }

    //TODO добавить валидацию
    public async Task EditArticleAsync(int articleId, string title, string text, string? shortDescription = null, string? previewPictureLink = null, List<string>? attachedPicturesLinks = null)
    {
        var article = new Article
        {
            Id = articleId,
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            PreviewPictureLink = previewPictureLink,
            AttachedPicturesLinks = attachedPicturesLinks
        };

        await _articleRepository.UpdateArticleAsync(article);
        await _unitOfWork.SaveChangesAsync();
    }
}