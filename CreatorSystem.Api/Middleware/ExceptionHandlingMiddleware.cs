using System.Net;
using System.Text.Json;

namespace CreatorSystem.Api.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An unhandled exception occurred: {Message}.", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = new { message = "An unexpected error occurred. Please try again later.", detail = ex.Message };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
