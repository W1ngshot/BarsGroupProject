using Core.CustomExceptions;

namespace WebApi.Middleware;

public class ExceptionMiddleware
{
    public readonly RequestDelegate Next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        Next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await Next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new {ex.Message});
        }
        catch (FluentValidation.ValidationException ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(ex.Errors.Select(x => x.ErrorMessage));
        }
        catch (AuthorizationException)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { Message = "Unauthorized" });
        }
        catch
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(new { Message = "Внутренняя ошибка сервера" });
        }
    }
}
