namespace WebApi.Middleware;

public class OptionsFakeMiddleware
{
    public readonly RequestDelegate Next;

    public OptionsFakeMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.Any(k => k.Key.Contains("Origin")) && context.Request.Method == "OPTIONS")
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("handled");
        }

        await Next(context);
    }
}