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

    public async Task<Article> GetArticleById(int articleId)
    {
        return await _articleRepository.GetArticleById(articleId);
    }

    //TODO добавить валидацию
    public async Task CreateArticle(string title, string shortDescription, string text, string? pictureLink, int userId)
    {
        var article = new Article
        {
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            PictureLink = pictureLink,
            UploadDate = DateTime.Now,
            UserId = userId
        };

        await _articleRepository.CreateArticle(article);
        await _unitOfWork.SaveChangesAsync();
    }

    //TODO добавить валидацию
    public async Task EditArticle(int articleId, string title, string shortDescription, string text, string? pictureLink)
    {
        var article = new Article
        {
            Id = articleId,
            Title = title,
            ShortDescription = shortDescription,
            Text = text,
            PictureLink = pictureLink,
        };

        await _articleRepository.UpdateArticle(article);
        await _unitOfWork.SaveChangesAsync();
    }
}