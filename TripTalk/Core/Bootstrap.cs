using Core.Services;
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
        services.AddScoped<ICryptographyService, CryptographyService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IRateService, RateService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<ISearchService, SearchService>();

        services.AddFluentValidation().AddValidatorsFromAssembly(typeof(AuthService).Assembly);
        return services;
    }
}