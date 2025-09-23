using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Dados de criação de uma Movimentação")]
public record MovimentacaoPostDto(
    [property: Description("Descrição da Movimentação")]
    string DescricaoMovimentacao,
    [property: Description("Identificador único da Moto")]
    int MotoId,
    [property: Description("Identificador único da Vaga")]
    int VagaId);