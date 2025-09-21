using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados resumidos do Pátio")]
public record PatioResumoDto(
    [property: Description("Identificador único de Pátio")]
    int PatioId,
    [property: Description("Localização do Pátio")]
    string LocalizacaoPatio,
    [property: Description("Nome do Pátio")]
    string NomePatio,
    [property: Description("Descrição do Pátio")]
    string DescricaoPatio)
{
    public static PatioResumoDto ToDto(Patio p) =>
        new(
            p.PatioId,
            p.LocalizacaoPatio,
            p.NomePatio,
            p.DescricaoPatio
        );
};