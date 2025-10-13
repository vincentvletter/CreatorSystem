namespace CreatorSystem.Application.Common.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; init; }
        public T? Data { get; init; }
        public string? Message { get; init; }
        public required string CorrelationId { get; init; } = Guid.NewGuid().ToString();
        public List<ApiError>? Errors { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;

        public static ApiResponse<T> SuccessResponse(T data, string? correlationId = null, string? message = null) => new ApiResponse<T>
        {
            Success = true,
            Data = data,
            Message = message,
            CorrelationId = correlationId ?? Guid.NewGuid().ToString()
        };

        public static ApiResponse<T> FailResponse(string message, string? correlationId = null, List<ApiError>? errors = null) => new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            CorrelationId = correlationId ?? Guid.NewGuid().ToString()
        };
    }

    public class ApiError
    {
        public required string Field { get; init; }
        public required string Message { get; init; }
    }
}
