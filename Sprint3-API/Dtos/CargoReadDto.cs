using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record CargoReadDto(
    int CargoId,
    string NomeCargo,
    string DescricaoCargo)
{
    public static CargoReadDto ToDto(Cargo c) =>
        new(
          c.CargoId,
          c.NomeCargo,
          c.DescricaoCargo
            );
};