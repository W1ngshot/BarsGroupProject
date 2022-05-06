using Core.Cryptography;
using Core.Domains.Article.Services;
using Core.Domains.Article.Services.Interfaces;
using Core.Domains.Comment.Services;
using Core.Domains.Comment.Services.Interfaces;
using Core.Domains.Rate.Services;
using Core.Domains.Rate.Services.Interfaces;
using Core.Domains.Tag.Services;
using Core.Domains.Tag.Services.Interfaces;
using Core.Domains.User.Services;
using Core.Domains.User.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Bootstrap
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IArticleService, ArticleService>();
        services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ISearchService, SearchService>();

        services.AddFluentValidation().AddValidatorsFromAssembly(typeof(AuthService).Assembly);
        return services;
    }
}