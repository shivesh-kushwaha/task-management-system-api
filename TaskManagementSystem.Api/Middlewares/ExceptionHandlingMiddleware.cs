namespace TaskManagementSystem.Api.Middlewares;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            string message = $"Unhandled exception occurred. Method: {context.Request.Method} Path: {context.Request.Path}";
            
            logger.LogError(ex, message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                Message = "An unexpected error occurred."
            });
        }
    }
}
