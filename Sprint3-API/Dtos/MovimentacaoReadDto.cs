using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura de uma Movimentação")]
public record MovimentacaoReadDto(
    [property: Description("Identificador único de Movimentação")]
    int MovimentacaoId,
    [property: Description("Data de Entrada")]
    DateTime DtEntrada,
    [property: Description("Data de Saída")]
    DateTime? DtSaida,
    [property: Description("Descrição da Movimentação")]
    string? DescricaoMovimentacao,
    [property: Description("Moto da Movimentação")]
    MotoReadDto Moto,
    [property: Description("Vaga da Movimentação")]
    VagaReadDto Vaga)
{
    public static MovimentacaoReadDto ToDto(Movimentacao m) => 
        new(
            m.MovimentacaoId,
            m.DtEntrada,
            m.DtSaida,
            m.DescricaoMovimentacao,
            MotoReadDto.ToDto(m.Moto),
            VagaReadDto.ToDto(m.Vaga)
        );
};