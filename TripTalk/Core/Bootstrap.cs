using Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Bootstrap
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddFluentValidation().AddValidatorsFromAssembly(typeof(AuthService).Assembly);
        return services;
    }
}