using Sprint3_API.Models;

namespace Sprint3_API.Dtos;

public record ClienteReadDto(
    int ClienteId,
    string NomeCliente,
    string TelefoneCliente,
    char SexoCliente,
    string EmailCliente,
    string CpfCliente,
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