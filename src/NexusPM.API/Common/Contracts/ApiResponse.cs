namespace NexusPM.API.Common.Contracts;

/// <summary>
/// Represents a standard API response containing data and metadata.
/// </summary>
public sealed record ApiResponse<T>(T Data, ResponseMeta Meta);

/// <summary>
/// Contains metadata for an API response, such as trace information and pagination.
/// </summary>
public sealed record ResponseMeta(string TraceId, DateTimeOffset Timestamp, PaginationMeta? Pagination = null);

/// <summary>
/// Contains pagination information for paged API responses.
/// </summary>
public sealed record PaginationMeta(int Page, int PageSize, int TotalItems, int TotalPages);
