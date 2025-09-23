using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Dados da última posição da Moto")]
public record UltimaPosicaoDto(
    [property: Description("Vaga da última posição da Moto")]
    VagaResumoDto Vaga,
    [property: Description("Setor da última posição da Moto")]
    SetorResumoDto Setor,
    [property: Description("Data de Entrada da Moto")]
    DateTime DtEntrada,
    [property: Description("Data de Saída da Moto")]
    DateTime? DtSaida,
    [property: Description("Moto permanece ou não")]
    bool Permanece
    );