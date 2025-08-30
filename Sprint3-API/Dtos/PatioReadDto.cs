using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record PatioReadDto(
    int PatioId,
    string LocalizacaoPatio,
    string NomePatio,
    string DescricaoPatio,
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