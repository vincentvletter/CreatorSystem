using CreatorSystem.Application.Common.Responses;
using FluentValidation;
using System.Net;
using System.Text.Json;

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

        ApiResponse<object> apiResponse;

        switch (ex)
        {
            case ValidationException validationEx:
                apiResponse = ApiResponse<object>.FailResponse(
                    "Validation error",
                    correlationId,
                    validationEx.Errors.Select(f => new ApiError
                    {
                        Field = f.PropertyName,
                        Message = f.ErrorMessage
                    })
                    .ToList()
                );
                response.StatusCode = StatusCodes.Status400BadRequest;
                break;

            case UnauthorizedAccessException:
                apiResponse = ApiResponse<object>.FailResponse($"Unauthorized: '{ex.Message}'", correlationId);
                response.StatusCode = StatusCodes.Status401Unauthorized;
                break;

            case KeyNotFoundException:
                apiResponse = ApiResponse<object>.FailResponse("Not Found", correlationId);
                response.StatusCode = StatusCodes.Status404NotFound;
                break;

            default:
                apiResponse = ApiResponse<object>.FailResponse($"An unexpected error occurred: '{ex.Message}'", correlationId);
                response.StatusCode = StatusCodes.Status500InternalServerError;
                break;
        }

        var json = JsonSerializer.Serialize(apiResponse, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await response.WriteAsync(json);
    }
}
