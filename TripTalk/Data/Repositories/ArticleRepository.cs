using Core.CustomExceptions;
using Core.CustomExceptions.Messages;
using Core.Domains.Article;
using Core.Domains.Article.Repository;
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

    public async Task<List<Article>> GetOrderedArticlesAsync(Category category, Period period, int count, int firstIndex)
    {
        var unorderedArticles = _context.Articles
            .AsNoTracking()
            .Where(article => (DateTime.UtcNow - article.UploadDate).TotalDays < (int)period);

        var orderedArticles = category switch
        {
            Category.Popular => unorderedArticles.OrderByDescending(article => article.Views),
            Category.Last => unorderedArticles.OrderByDescending(article => article.UploadDate),
            Category.Best => unorderedArticles.Include(article => article.Rates)
                .OrderByDescending(article => article.Rates.Sum(rate => rate.Rating)),
            _ => throw new ValidationException(ErrorMessages.InvalidCategoryValue)
        };

        var articleModelList = await orderedArticles
            .Include(article => article.User)
            .Include(article => article.Tags)
            .Include(article => article.Rates)
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
            Rating = entity.Rates.Sum(rate => rate.Rating),
            Views = entity.Views,
            Tags = entity.Tags.Select(tag => tag.Name).ToList(),
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        }).ToList();
    }

    public async Task<int> GetArticlesCountAsync()
    {
        return await _context.Articles.CountAsync();
    }

    public async Task<List<Article>> GetFilteredArticlesAsync(string searchLine, List<string>? tags, int count, int firstIndex)
    {
        var filteredArticles = _context.Articles
            .AsNoTracking()
            .Where(article => article.Title.Contains(searchLine));
        if (tags is not null)
            filteredArticles = filteredArticles.Where(article => article.Tags.Any(tag => tags.Contains(tag.Name)));

        var articleModelList = await filteredArticles
            .Include(article => article.User)
            .Include(article => article.Tags)
            .Include(article => article.Rates)
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
            Rating = entity.Rates.Sum(rate => rate.Rating),
            Views = entity.Views,
            Tags = entity.Tags.Select(tag => tag.Name).ToList(),
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        }).ToList();
    }

    public async Task<int> GetFilteredArticlesCountAsync(string searchLine, List<string>? tags)
    {
        var filteredArticles = _context.Articles
            .Where(article => article.Title.Contains(searchLine));
        if (tags is not null)
            filteredArticles = filteredArticles.Where(article => article.Tags.Any(tag => tags.Contains(tag.Name)));
        return await filteredArticles.CountAsync();
    }

    public async Task<int> GetUserArticlesCountAsync(int userId)
    {
        return await _context.Articles
            .Where(article => article.UserId == userId)
            .CountAsync();
    }

    public async Task<List<Article>> GetUserArticlesAsync(int userId, int count, int firstIndex)
    {
        var articleModelList = await _context.Articles
            .AsNoTracking()
            .Where(article => article.UserId == userId)
            .Include(article => article.User)
            .Include(article => article.Tags)
            .Include(article => article.Rates)
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
            Rating = entity.Rates.Sum(rate => rate.Rating),
            Views = entity.Views,
            Tags = entity.Tags.Select(tag => tag.Name).ToList(),
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        }).ToList();
    }

    public async Task<Article> GetArticleByIdAsync(int id)
    {
        var entity = await _context.Articles.Include(article => article.User)
                         .Include(article => article.Tags)
                         .Include(article => article.Rates)
                         .FirstOrDefaultAsync(article => article.Id == id) ??
            throw new ValidationException(ErrorMessages.MissingArticle);
        return new Article
        {
            Id = entity.Id,
            Title = entity.Title,
            ShortDescription = entity.ShortDescription,
            Text = entity.Text,
            UploadDate = entity.UploadDate,
            UserId = entity.UserId,
            PreviewPictureLink = entity.AssetLink,
            Rating = entity.Rates.Sum(rate => rate.Rating),
            Views = entity.Views,
            Tags = entity.Tags.Select(tag => tag.Name).ToList(),
            UserNickname = entity.User.Nickname,
            UserAvatarLink = entity.User.AvatarLink
        };
    }

    public async Task<int> AddArticleAsync(Article article)
    {
        var entity = new ArticleDbModel
        {
            Title = article.Title,
            ShortDescription = article.ShortDescription,
            Text = article.Text,
            UploadDate = article.UploadDate,
            UserId = article.UserId,
            AssetLink = article.PreviewPictureLink,
            Views = article.Views
        };
        await _context.Articles.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateArticleAsync(Article article)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == article.Id) ??
            throw new ValidationException(ErrorMessages.MissingArticle);

        entity.Title = article.Title;
        entity.ShortDescription = article.ShortDescription;
        entity.Text = article.Text;
        entity.AssetLink = article.PreviewPictureLink;
        entity.Views = article.Views;
    }

    public async Task RemoveArticleAsync(int id)
    {
        var entity = await _context.Articles.FirstOrDefaultAsync(a => a.Id == id) ??
            throw new ValidationException(ErrorMessages.MissingArticle);

        _context.Articles.Remove(entity);
    }
}