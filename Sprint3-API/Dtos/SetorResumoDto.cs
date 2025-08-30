using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record SetorResumoDto(
    int SetorId,
    string TipoSetor,
    string StatusSetor,
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