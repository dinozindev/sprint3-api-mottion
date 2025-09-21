using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura do Setor")]
public record SetorReadDto(
    [property: Description("Identificador único de Setor")]
    int SetorId,
    [property: Description("Tipo de Setor")]
    string TipoSetor,
    [property: Description("Status de Setor")]
    string StatusSetor,
    [property: Description("Patio do Setor")]
    PatioResumoDto Patio,
    [property: Description("Lista de Vagas de um Setor")]
    List<VagaResumoDto> Vagas
    )
{
    public static SetorReadDto ToDto(Setor s) =>
        new(
            s.SetorId,
            s.TipoSetor,
            s.StatusSetor,
            PatioResumoDto.ToDto(s.Patio),
            s.Vagas.Select(VagaResumoDto.ToDto).ToList()
        );
};