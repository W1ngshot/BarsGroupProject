using Core.Models;
using Core.RepositoryInterfaces;
using Data.Db;
using Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly TripTalkContext _context;

    public ArticleRepository(TripTalkContext context)
    {
        _context = context;
    }

    //TODO разобраться с ссылками на изображения
    public async Task<Article> GetArticleByIdAsync(int id)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(article => article.Id == id) ??
                             throw new Exception("Данного пользователя не существует"); //TODO подумать как исправить exception
        return new Article
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            Text = entity.Text,
            UploadDate = entity.UploadDate,
            UserId = entity.UserId,
            PreviewPictureLink = entity.AssetLink,
            Rating = entity.Rating,
            Views = entity.Views
        };
    }

    //TODO разобраться с ссылками на изображения
    public async Task CreateArticleAsync(Article article)
    {
        var entity = new ArticleDbModel
        {
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            Text = article.Text,
            UploadDate = article.UploadDate,
            UserId = article.UserId,
            AssetLink = article.PreviewPictureLink,
            Rating = article.Rating,
            Views = article.Views
        };
        await _context.Articles.AddAsync(entity);
    }

    //TODO разобраться с ссылками на изображения
    public async Task UpdateArticleAsync(Article article)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id) ??
                     throw new Exception("Данного пользователя не существует"); //TODO подумать как исправить exception

        entity.Title = article.Title;
        entity.ShortDescription = article.ShortDescription;
        entity.Text = article.Text;
        entity.AssetLink = article.PreviewPictureLink;
        entity.Rating = article.Rating;
        entity.Views = article.Views;
    }

    //TODO использовать модели, чтобы не передавать весь элемент
    //TODO разобраться с ссылками на изображения
    public async Task<List<Article>> GetUserArticlesAsync(int userId)
    {
        var articleModelList = await _context.Articles
            .AsNoTracking()
            .Where(article => article.UserId == userId)
            .ToListAsync();

        return articleModelList.Select(article => new Article
        {
            Id = article.Id,
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            Text = article.Text,
            UploadDate = article.UploadDate,
            UserId = article.UserId,
            PreviewPictureLink = article.AssetLink,
            Rating = article.Rating,
            Views = article.Views
        }).ToList();
    }
}