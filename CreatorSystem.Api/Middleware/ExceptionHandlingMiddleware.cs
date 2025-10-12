using System.Net;
using System.Text.Json;
using FluentValidation;

namespace CreatorSystem.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var correlationId = Guid.NewGuid().ToString();
        _logger.LogError(ex, "❌ Unhandled exception: {Message} | CorrelationId: {CorrelationId}", ex.Message, correlationId);

        var response = context.Response;
        response.ContentType = "application/json";

        var problem = ex switch
        {
            // 🔹 FluentValidation exceptions
            ValidationException validationEx => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Title = "Validation Error",
                Errors = validationEx.Errors
                    .Select(f => new ApiValidationError
                    {
                        Field = f.PropertyName,
                        Message = f.ErrorMessage
                    })
                    .ToList(),
                CorrelationId = correlationId
            },

            // 🔹 Duplicate / business conflict
            InvalidOperationException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Conflict,
                Title = "Conflict",
                Detail = ex.Message,
                CorrelationId = correlationId
            },

            // 🔹 Invalid argument / bad input
            ArgumentException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Title = "Bad Request",
                Detail = ex.Message,
                CorrelationId = correlationId
            },

            // 🔹 Unauthorized access
            UnauthorizedAccessException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Title = "Unauthorized",
                Detail = ex.Message,
                CorrelationId = correlationId
            },

            // 🔹 Not found (optioneel: eigen NotFoundException maken)
            KeyNotFoundException => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Title = "Not Found",
                Detail = ex.Message,
                CorrelationId = correlationId
            },

            // 🔹 Default fallback
            _ => new ApiErrorResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Title = "Internal Server Error",
                Detail = "An unexpected error occurred. Please try again later.",
                CorrelationId = correlationId
            }
        };

        response.StatusCode = problem.StatusCode;

        var json = JsonSerializer.Serialize(problem, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await response.WriteAsync(json);
    }
}

/// <summary>
/// Generic structured API error response for consistent output
/// </summary>
public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Title { get; set; } = default!;
    public string? Detail { get; set; }
    public List<ApiValidationError>? Errors { get; set; }
    public string CorrelationId { get; set; } = default!;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Represents a single validation error (field + message)
/// </summary>
public class ApiValidationError
{
    public string Field { get; set; } = default!;
    public string Message { get; set; } = default!;
}
