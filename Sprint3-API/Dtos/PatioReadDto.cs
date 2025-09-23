using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura do Pátio")]
public record PatioReadDto(
    [property: Description("Identificador único de Pátio")]
    int PatioId,
    [property: Description("Localização do Pátio")]
    string LocalizacaoPatio,
    [property: Description("Nome do Pátio")]
    string NomePatio,
    [property: Description("Descrição do Pátio")]
    string DescricaoPatio,
    [property: Description("Lista de Setores do Pátio")]
    List<SetorResumoPatioDto> Setores
)
{
    public static PatioReadDto ToDto(Patio p) =>
        new(
            p.PatioId,
            p.LocalizacaoPatio,
            p.NomePatio,
            p.DescricaoPatio,
            p.Setores.Select(SetorResumoPatioDto.ToDto).ToList()
            );
};