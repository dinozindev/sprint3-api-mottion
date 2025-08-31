namespace Sprint3_API.Dtos;

public record ResourceResponse<T>(
    T Data,
    IEnumerable<LinkDto> Links
    );
