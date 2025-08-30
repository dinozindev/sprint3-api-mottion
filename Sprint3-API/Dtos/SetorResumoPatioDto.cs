using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record SetorResumoPatioDto(
    int SetorId,
    string TipoSetor,
    string StatusSetor,
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