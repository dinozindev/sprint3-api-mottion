using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record PatioResumoDto(
    int PatioId,
    string LocalizacaoPatio,
    string NomePatio,
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