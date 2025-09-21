using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Total de Vagas e Motos em um Setor")]
public record VagasSetorDto(
    [property: Description("Setor")]
    string Setor,
    [property: Description("Total de Vagas no Setor")]
    int TotalVagas,
    [property: Description("Total de Motos presentes no Setor")]
    int MotosPresentes
    );