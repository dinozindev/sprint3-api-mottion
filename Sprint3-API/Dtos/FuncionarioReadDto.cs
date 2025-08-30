using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record FuncionarioReadDto(
    int FuncionarioId,
    string NomeFuncionario,
    string TelefoneFuncionario,
    CargoReadDto Cargo,
    PatioResumoDto Patio)
{
    public static FuncionarioReadDto ToDto(Funcionario f) =>
        new(
        f.FuncionarioId,
        f.NomeFuncionario,
        f.TelefoneFuncionario,
        CargoReadDto.ToDto(f.Cargo),
        PatioResumoDto.ToDto(f.Patio)
        );
};