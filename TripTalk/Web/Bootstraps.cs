using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Web.Hosted_Services;

namespace Web;

public static class Bootstraps
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TripTalkContext>(options => options
            .UseNpgsql(configuration.GetConnectionString("DbConnection"))
            .UseSnakeCaseNamingConvention());
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