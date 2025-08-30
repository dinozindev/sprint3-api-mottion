using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record ClienteResumoDto(
    int ClienteId,
    string NomeCliente,
    string TelefoneCliente,
    char SexoCliente,
    string EmailCliente,
    string CpfCliente)
{
    public static ClienteResumoDto ToDto(Cliente c) =>
        new(
            c.ClienteId,
            c.NomeCliente,
            c.TelefoneCliente,
            c.SexoCliente,
            c.EmailCliente,
            c.CpfCliente
        );
};