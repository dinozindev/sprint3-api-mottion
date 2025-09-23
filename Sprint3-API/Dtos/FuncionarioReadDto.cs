using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura do Funcionário")]
public record FuncionarioReadDto(
    [property: Description("Identificador único do Funcionário")]
    int FuncionarioId,
    [property: Description("Nome do Funcionário")]
    string NomeFuncionario,
    [property: Description("Telefone do Funcionário")]
    string TelefoneFuncionario,
    [property: Description("Cargo do Funcionário")]
    CargoReadDto Cargo,
    [property: Description("Pátio do Funcionário")]
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