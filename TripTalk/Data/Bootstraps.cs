using Core;
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

        return services;
    }
}