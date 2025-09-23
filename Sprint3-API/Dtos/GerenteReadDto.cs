using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura do Gerente")]
public record GerenteReadDto(
    [property: Description("Identificador único do Gerente")]
    int GerenteId,
    [property: Description("Nome do Gerente")]
    string NomeGerente,
    [property: Description("Telefone do Gerente")]
    string TelefoneGerente,
    [property: Description("CPF do Gerente")]
    string CpfGerente,
    [property: Description("Pátio do Gerente")]
    PatioResumoDto Patio)
{
    public static GerenteReadDto ToDto(Gerente g) =>
        new(
            g.GerenteId,
            g.NomeGerente,
            g.TelefoneGerente,
            g.CpfGerente,
            PatioResumoDto.ToDto(g.Patio)
        );
};