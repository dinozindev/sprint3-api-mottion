using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura da Moto")]
public record MotoReadDto(
    [property: Description("Identificador único da Moto")]
    int MotoId,
    [property: Description("Placa da Moto")]
    string? PlacaMoto,
    [property: Description("Modelo da Moto")]
    string ModeloMoto,
    [property: Description("Situação da Moto")]
    string SituacaoMoto,
    [property: Description("Chassi da Moto")]
    string ChassiMoto,
    [property: Description("Cliente da Moto")]
    ClienteResumoDto? Cliente)
{
    public static MotoReadDto ToDto(Moto m) =>
        new(
            m.MotoId,
            m.PlacaMoto,
            m.ModeloMoto,
            m.SituacaoMoto,
            m.ChassiMoto,
            m.Cliente == null 
                ? null 
                : ClienteResumoDto.ToDto(m.Cliente)
        );
};