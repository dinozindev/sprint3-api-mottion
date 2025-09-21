using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados do Setor resumidos para o Pátio")]
public record SetorResumoPatioDto(
    
    [property: Description("Identificador único de Setor")]
    int SetorId,
    [property: Description("Tipo de Setor")]
    string TipoSetor,
    [property: Description("Status de Setor")]
    string StatusSetor,
    [property: Description("Lista de Vagas de um Setor")]
    List<VagaResumoDto> Vagas
)
{
    public static SetorResumoPatioDto ToDto(Setor s) =>
        new(
            s.SetorId,
            s.TipoSetor,
            s.StatusSetor,
            s.Vagas.Select(VagaResumoDto.ToDto).ToList()
            );
};