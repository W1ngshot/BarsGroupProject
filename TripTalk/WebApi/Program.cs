using Core;
using Data;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using WebApi.Authentication;
using WebApi.HostedServices;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerWithAuthOptions();
builder.Services.AddCore();
builder.Services.AddData(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddHostedService<MigrationHostedService>();
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "ClientApp/dist/bars-frontend";
});

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<OptionsFakeMiddleware>();
app.UseMiddleware<JwtAuthenticationMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "ClientApp";

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.Run();
