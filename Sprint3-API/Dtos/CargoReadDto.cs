using System.ComponentModel;
using Sprint3_API.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint3_API.Dtos;

[Description("Dados de um cargo")]
public record CargoReadDto(
    [property: Description("Identificador único de um Cargo")]
    int CargoId,
    [property: Description("Nome de um Cargo")]
    string NomeCargo,
    [property: Description("Descrição de um Cargo")]
    string DescricaoCargo)
{
    public static CargoReadDto ToDto(Cargo c) =>
        new(
          c.CargoId,
          c.NomeCargo,
          c.DescricaoCargo
            );
};