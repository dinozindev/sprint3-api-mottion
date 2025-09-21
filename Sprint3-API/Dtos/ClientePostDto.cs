using System.ComponentModel;

namespace Sprint3_API.Dtos;

[Description("Dados de criação de um Cliente")]
public record ClientePostDto(
    [property: Description("Nome do Cliente")]
    string NomeCliente,
    [property: Description("Telefone do Cliente")]
    string TelefoneCliente,
    [property: Description("Sexo do Cliente")]
    char SexoCliente,
    [property: Description("Email do Cliente")]
    string EmailCliente,
    [property: Description("CPF do Cliente")]
    string CpfCliente);