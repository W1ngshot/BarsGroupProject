using Core;
using Core.RepositoryInterfaces;
using Data.Db;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class Bootstraps
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TripTalkContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("DbConnection"))
            .UseSnakeCaseNamingConvention());
        services.AddScoped<IUnitOfWork, EfUnitOfWork>();

        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IRateRepository, RateRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        return services;
    }
}