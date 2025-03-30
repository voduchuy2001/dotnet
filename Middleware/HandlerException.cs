using System.Net;
using System.Text.Json;

namespace Api.Middleware;

public class HandlerException(RequestDelegate next, ILogger<HandlerException> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<HandlerException> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        var response = context.Response;
        response.ContentType = "application/json";

        var (statusCode, message) = exception switch
        {
            UnauthorizedAccessException => (HttpStatusCode.Unauthorized, exception.Message),
            ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
            KeyNotFoundException => (HttpStatusCode.NotFound, "Resource not found."),
            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
        };

        response.StatusCode = (int)statusCode;
        var result = JsonSerializer.Serialize(new { Message = message });
        await response.WriteAsync(result);
    }
}