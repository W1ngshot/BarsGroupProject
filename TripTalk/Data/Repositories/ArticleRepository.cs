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
    public async Task<List<Article>> GetOrderedArticlesAsync(Category category, Period period, int count, int firstIndex)
    {
        var unorderedArticles = _context.Articles
            .AsNoTracking()
            .Where(article => (DateTime.UtcNow - article.UploadDate).TotalDays < (int)period);

        var orderedArticles = category switch
        {
            Category.Popular => unorderedArticles.OrderByDescending(article => article.Views),
            Category.Last => unorderedArticles.OrderByDescending(article => article.UploadDate),
            Category.Best => unorderedArticles.OrderByDescending(article => article.Rating),
            _ => throw new Exception(ErrorMessages.InvalidCategoryValue)
        };

        var articleModelList = await orderedArticles
            .Skip(firstIndex)
            .Take(count)
            .ToListAsync();

        return articleModelList.Select(entity => new Article
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
        }).ToList();
    }

    public async Task<List<Article>> GetFilteredArticlesAsync(string searchLine, List<string>? tags, int count, int firstIndex)
    {
        var filteredArticles = _context.Articles
            .AsNoTracking()
            .Where(article => article.Title.Contains(searchLine));
        if (tags is not null)
            filteredArticles = filteredArticles.Where(article => article.Tags.Any(tag => tags.Contains(tag.Name)));

        var articleModelList = await filteredArticles
            .Skip(firstIndex)
            .Take(count)
            .ToListAsync();

        return articleModelList.Select(entity => new Article
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
        }).ToList();
    }

    //TODO использовать модели, чтобы не передавать весь элемент
    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count, int firstIndex)
    {
        var articleModelList = await _context.Articles
            .AsNoTracking()
            .Where(article => article.UserId == userId)
            .Skip(firstIndex)
            .Take(count)
            .ToListAsync();

        return articleModelList.Select(entity => new Article
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
        }).ToList();
    }

    public async Task<Article> GetArticleByIdAsync(int id)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(article => article.Id == id) ??
            throw new Exception(ErrorMessages.MissingUser); //TODO подумать как исправить exception
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

    public async Task AddArticleAsync(Article article)
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

    public async Task UpdateArticleAsync(Article article)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id) ??
            throw new Exception(ErrorMessages.MissingUser); //TODO подумать как исправить exception

        entity.Title = article.Title;
        entity.ShortDescription = article.ShortDescription;
        entity.Text = article.Text;
        entity.AssetLink = article.PreviewPictureLink;
        entity.Rating = article.Rating;
        entity.Views = article.Views;
    }

    public async Task RemoveArticleAsync(int id)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id) ??
            throw new Exception(ErrorMessages.MissingArticle);

        _context.Articles.Remove(entity);
    }
}