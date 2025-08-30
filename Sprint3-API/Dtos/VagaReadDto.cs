using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record VagaReadDto(
    int VagaId,
    string NumeroVaga,
    int StatusOcupada,
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