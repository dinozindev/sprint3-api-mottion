using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados resumidos de Setor")]
public record SetorResumoDto(
    [property: Description("Identificador único de Setor")]
    int SetorId,
    [property: Description("Tipo de Setor")]
    string TipoSetor,
    [property: Description("Status de Setor")]
    string StatusSetor,
    [property: Description("Identificador único de Pátio")]
    int PatioId)
{
    public static SetorResumoDto ToDto(Setor s) =>
        new(
            s.SetorId,
            s.TipoSetor,
            s.StatusSetor,
            s.PatioId
        );
}