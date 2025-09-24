using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Resposta padrão de um recurso com links HATEOAS")]
public record ResourceResponse<T>(
    [Description("Dados do recurso")]
    T Data,
    [Description("Links HATEOAS relacionados ao recurso")]
    IEnumerable<LinkDto> Links
    );
