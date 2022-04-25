using Core;
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

    //TODO использовать модели, чтобы не передавать весь элемент
    //TODO использовать свойства статьи Views и Rating вместо заглушки
    public async Task<List<Article>> GetCategoryArticlesAsync(Category category, Period period, int count, int firstIndex)
    {
        var unorderedArticles = _context.Articles
            .AsNoTracking()
            .Where(a => (DateTime.UtcNow - a.UploadDate).TotalDays < (int)period);

        var orderedArticles = category switch
        {
            Category.Popular => unorderedArticles.OrderByDescending(a => a.UploadDate), //a.Views вместо a.UploadDate
            Category.Last => unorderedArticles.OrderByDescending(a => a.UploadDate),
            Category.Best => unorderedArticles.OrderByDescending(a => a.UploadDate) //a.Rating вместо a.UploadDate
        };

        var articleModelList = await orderedArticles
            .Skip(firstIndex)
            .Take(count)
            .ToListAsync();

        return articleModelList.Select(a => new Article
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Text = a.Text,
            PictureLink = a.AssetLink,
            UploadDate = a.UploadDate,
            UserId = a.UserId
        }).ToList();
    }

    //TODO использовать модели, чтобы не передавать весь элемент
    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count, int firstIndex)
    {
        var articleModelList = await _context.Articles
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .Skip(firstIndex)
            .Take(count)
            .ToListAsync();

        return articleModelList.Select(a => new Article
        {
            Id = a.Id,
            Title = a.Title,
            ShortDescription = a.ShortDescription,
            Text = a.Text,
            PictureLink = a.AssetLink,
            UploadDate = a.UploadDate,
            UserId = a.UserId
        }).ToList();
    }

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
            PictureLink = entity.AssetLink,
            UploadDate = entity.UploadDate,
            UserId = entity.UserId
        };
    }

    public async Task CreateArticleAsync(Article article)
    {
        var entity = new ArticleDbModel
        {
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            Text = article.Text,
            AssetLink = article.PictureLink,
            UploadDate = article.UploadDate,
            UserId = article.UserId
        };
        await _context.Articles.AddAsync(entity);
    }

    public async Task UpdateArticleAsync(Article article)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id) ??
                     throw new Exception("Данного пользователя не существует"); //TODO подумать как исправить exception

        entity.Title = article.Title;
        entity.ShortDescription = article.ShortDescription;
        entity.Text = article.Text;
        entity.AssetLink = article.PictureLink;
    }
}