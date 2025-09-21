using System.ComponentModel;
using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

[Description("Dados de leitura de um Cliente")]
public record ClienteReadDto(
    [property: Description("Identificador único do Cliente")]
    int ClienteId,
    [property: Description("Nome do Cliente")]
    string NomeCliente,
    [property: Description("Telefone do Cliente")]
    string TelefoneCliente,
    [property: Description("Sexo do Cliente")]
    char SexoCliente,
    [property: Description("Email do Cliente")]
    string EmailCliente,
    [property: Description("CPF do Cliente")]
    string CpfCliente,
    [property: Description("Lista de Motos do Cliente")]
    List<MotoResumoDto>? Motos
    )
{
    public static ClienteReadDto ToDto(Cliente c) =>
        new(
            c.ClienteId,
            c.NomeCliente,
            c.TelefoneCliente,
            c.SexoCliente,
            c.EmailCliente,
            c.CpfCliente,
            c.Motos?.Select(m => MotoResumoDto.ToDto(m)).ToList() ?? new List<MotoResumoDto>()
        );
};