namespace Sprint3_API.Dtos;

public record ClientePostDto(
    string NomeCliente,
    string TelefoneCliente,
    char SexoCliente,
    string EmailCliente,
    string CpfCliente);