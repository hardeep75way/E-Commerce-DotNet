using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace E_Commerce.Middleware;

/// <summary>
/// Global exception handler middleware
/// Catches all unhandled exceptions and provides user-friendly error responses
/// Logs exceptions for debugging and monitoring
/// </summary>
public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred. RequestId: {RequestId}", 
                Activity.Current?.Id ?? context.TraceIdentifier);
            
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // For AJAX requests, return JSON error
        if (context.Request.Headers["X-Requested-With"] == "XMLHttpRequest" ||
            context.Request.Headers["Accept"].ToString().Contains("application/json"))
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new
            {
                error = "An unexpected error occurred. Please try again later.",
                requestId = Activity.Current?.Id ?? context.TraceIdentifier
            };

            return context.Response.WriteAsJsonAsync(response);
        }

        // For regular requests, redirect to error page
        context.Response.Redirect("/Home/Error");
        return Task.CompletedTask;
    }
}

/// <summary>
/// Extension method to register global exception handler
/// </summary>
public static class GlobalExceptionHandlerExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionHandler>();
    }
}
