using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura da Vaga")]
public record VagaReadDto(
    [property: Description("Identificador único da Vaga")]
    int VagaId,
    [property: Description("Número da Vaga")]
    string NumeroVaga,
    [property: Description("Status da Vaga")]
    int StatusOcupada,
    [property: Description("Setor da Vaga")]
    SetorResumoDto Setor)
{
    public static VagaReadDto ToDto(Vaga v) =>
        new(
            v.VagaId,
            v.NumeroVaga,
            v.StatusOcupada,
            SetorResumoDto.ToDto(v.Setor)
        );
};