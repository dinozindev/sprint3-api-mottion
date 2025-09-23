using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Dados de Criação de uma Moto")]
public record MotoPostDto(
    [property: Description("Placa da Moto")]
    string? PlacaMoto,
    [property: Description("Modelo da Moto")]
    string ModeloMoto,
    [property: Description("Situação da Moto")]
    string SituacaoMoto,
    [property: Description("Chassi da Moto")]
    string ChassiMoto);