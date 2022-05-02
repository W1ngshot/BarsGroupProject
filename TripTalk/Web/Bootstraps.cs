using Microsoft.AspNetCore.Authentication.Cookies;
using OldWeb.Hosted_Services;

namespace OldWeb;

public static class Bootstraps
{
    public static IServiceCollection AddWeb(this IServiceCollection services)
    {
        services.AddHostedService<MigrationHostedService>();


        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => //CookieAuthenticationOptions
            {
                options.LoginPath = new PathString("/Auth/Login");
            });
        services.AddControllersWithViews();
        return services;
    }
}