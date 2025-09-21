namespace NexusPM.API.Common.Contracts;

/// <summary>
/// Provides factory methods for creating standardized <see cref="ApiResponse{T}"/> objects.
/// </summary>
public static class ApiResponseFactory
{
    /// <summary>
    /// Creates an <see cref="ApiResponse{T}"/> representing a successful response with the specified data and optional pagination metadata.
    /// </summary>
    /// <typeparam name="T">The type of the response data.</typeparam>
    /// <param name="http">The current HTTP context.</param>
    /// <param name="data">The response data.</param>
    /// <param name="pagination">Optional pagination metadata.</param>
    /// <returns>An <see cref="ApiResponse{T}"/> containing the data and response metadata.</returns>
    public static ApiResponse<T> Ok<T>(HttpContext http, T data, PaginationMeta? pagination = null)
        => new (data, new ResponseMeta(
            TraceId: http.TraceIdentifier,
            Timestamp: DateTimeOffset.UtcNow,
            Pagination: pagination));
}
