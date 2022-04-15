using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Bootstrap
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}