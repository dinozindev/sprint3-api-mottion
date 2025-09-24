using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Representa um Link HATEOAS")]
public record LinkDto(
    [property: Description("Relação do link (ex: self, list, next, prev...)")]
    string Rel,
    [property: Description("URL do recurso")]
    string Href,
    [property: Description("Método HTTP associado ao link (GET, POST, PUT, DELETE)")]
    string Method
);