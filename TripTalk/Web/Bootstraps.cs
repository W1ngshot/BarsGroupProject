using Microsoft.AspNetCore.Authentication.Cookies;
using Web.Hosted_Services;

namespace Web;

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