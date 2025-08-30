namespace Sprint3_API.Dtos;

public record UltimaPosicaoDto(
    VagaResumoDto Vaga,
    SetorResumoDto Setor,
    DateTime DtEntrada,
    DateTime? DtSaida,
    bool Permanece
    );