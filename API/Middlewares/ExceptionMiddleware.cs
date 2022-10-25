using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(
        RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            // _logger.LogError(e, e.Message);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = _env.IsDevelopment()
                ? new ApiExceptions(httpContext.Response.StatusCode, e.Message, e.StackTrace?.ToString())
                : new ApiExceptions(httpContext.Response.StatusCode, "Internal Server Error!");
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var json = JsonSerializer.Serialize(response, options);
            await httpContext.Response.WriteAsync(json);
        }
    }
}