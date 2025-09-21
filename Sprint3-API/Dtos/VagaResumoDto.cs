using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados resumidos da Vaga")]
public record VagaResumoDto(
    [property: Description("Identificador único da Vaga")]
    int VagaId,
    [property: Description("Número da Vaga")]
    string NumeroVaga,
    [property: Description("Status da Vaga")]
    int StatusOcupada
    )
{
    public static VagaResumoDto ToDto(Vaga v) =>
        new(
            v.VagaId,
            v.NumeroVaga,
            v.StatusOcupada
        );
};