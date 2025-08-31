namespace Sprint3_API.Dtos;

public record PagedResponse<T>(
    int TotalCount,
    int PageNumber,
    int PageSize,
    int TotalPages,
    IEnumerable<T> Data,
    IEnumerable<LinkDto> Links
    );