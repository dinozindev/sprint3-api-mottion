using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;
using Sprint3_API.Models;

namespace Sprint3_API.Services;

public class ClienteService
{
    private readonly AppDbContext _db;
    private const string clientesString = "/clientes";

    public ClienteService(AppDbContext db)
    {
        _db = db;
    }
    
    
    
    // retorna todos os clientes
    public async Task<IResult> GetAllClientesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Clientes.CountAsync();
        
        var clientes = await _db.Clientes
            .Include(c => c.Motos)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var clientesDto = clientes.Select(ClienteReadDto.ToDto).ToList();

        var response = new PagedResponse<ClienteReadDto>
        (
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: clientesDto,
            Links: new List<LinkDto>
            {
                new("self", $"/clientes?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("create", clientesString, "POST"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/clientes?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/clientes?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
        );
        
        return clientesDto.Count != 0 ? Results.Ok(response) : Results.NoContent();
    }

    // busca o cliente pelo ID
    public async Task<IResult> GetClienteByIdAsync(int id)
    {
        var cliente = await _db.Clientes.Include(c => c.Motos).FirstOrDefaultAsync(c => c.ClienteId == id);
        if (cliente is null) return Results.NotFound("Cliente não encontrado com ID informado.");
        
        var clienteDto = ClienteReadDto.ToDto(cliente);

        var response = new ResourceResponse<ClienteReadDto>(
            Data: clienteDto,
            Links: new List<LinkDto>
            {
                new("self", $"/clientes/{id}", "GET"),
                new("update", $"/clientes/{id}", "PUT"),
                new("delete", $"/clientes/{id}", "DELETE"),
                new("list", clientesString, "GET")
            }
            );
        
        return Results.Ok(response);
    }

    // cria um novo cliente
    public async Task<IResult> CreateClienteAsync(ClientePostDto dto)
    {
        var validation = await ValidateCliente(dto);
        if (validation is not null) return validation;
        
        var cliente = new Cliente
        {
            TelefoneCliente = dto.TelefoneCliente,
            NomeCliente = dto.NomeCliente,
            SexoCliente = dto.SexoCliente,
            EmailCliente = dto.EmailCliente,
            CpfCliente = dto.CpfCliente,
            Motos = new List<Moto>()
        };
        
        _db.Clientes.Add(cliente);
        await _db.SaveChangesAsync();
        
        var clienteDto = ClienteReadDto.ToDto(cliente);

        var response = new ResourceResponse<ClienteReadDto>(
            Data: clienteDto,
            Links: new List<LinkDto>
            {
                new("self", $"/clientes/{clienteDto.ClienteId}", "GET"),
                new("update", $"/clientes/{clienteDto.ClienteId}", "PUT"),
                new("delete", $"/clientes/{clienteDto.ClienteId}", "DELETE"),
                new("list", clientesString, "GET")
            });
        
        return Results.Created($"/clientes/{cliente.ClienteId}", response);
    }

    // atualiza os dados do cliente
    public async Task<IResult> UpdateClienteAsync(int id, ClientePostDto dto)
    {
        var clienteExistente = await _db.Clientes.FindAsync(id);
        if (clienteExistente is null) return Results.NotFound("Cliente não encontrado com ID informado.");
        
        var validation = await ValidateCliente(dto);
        if (validation is not null) return validation;
        
        clienteExistente.NomeCliente = dto.NomeCliente;
        clienteExistente.TelefoneCliente = dto.TelefoneCliente;
        clienteExistente.SexoCliente = dto.SexoCliente;
        clienteExistente.EmailCliente = dto.EmailCliente;
        clienteExistente.CpfCliente = dto.CpfCliente;

        await _db.SaveChangesAsync();
        
        var clienteDto = ClienteReadDto.ToDto(clienteExistente);

        var response = new ResourceResponse<ClienteReadDto>(
            Data: clienteDto,
            Links: new List<LinkDto>
            {
                new("self", $"/clientes/{clienteDto.ClienteId}", "GET"),
                new("update", $"/clientes/{id}", "PUT"),
                new("delete", $"/clientes/{clienteDto.ClienteId}", "DELETE"),
                new("list", clientesString, "GET")
            });
        
        return Results.Ok(response);
    }

    // deleta um cliente pelo ID
    public async Task<IResult> DeleteClienteAsync(int id)
    {
        var cliente = await _db.Clientes.FindAsync(id);
        if (cliente is null) return Results.NotFound("Cliente não encontrado com ID informado.");
        
        _db.Clientes.Remove(cliente);
        await _db.SaveChangesAsync();
        return Results.NoContent();
    }

    // valida as informações do cliente para POST e PUT
    private async Task<IResult?> ValidateCliente(ClientePostDto dto, int? ignoreId = null)
    {
        // verifica se o telefone está no formato correto
        if (!string.IsNullOrWhiteSpace(dto.TelefoneCliente) &&
            !System.Text.RegularExpressions.Regex.IsMatch(dto.TelefoneCliente, "^[0-9]{0,11}$", RegexOptions.None, TimeSpan.FromMilliseconds(100)))
        { 
            return Results.BadRequest("Telefone no formato inválido.");
        }
    
        // verifica se o Email está no formato correto
        if (!string.IsNullOrWhiteSpace(dto.EmailCliente) &&
            !System.Text.RegularExpressions.Regex.IsMatch(dto.EmailCliente,
                "^(?=.{1,100}$)[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,63}$", RegexOptions.None, TimeSpan.FromMilliseconds(100)))
        {
            return Results.BadRequest("E-mail no formato inválido.");
        }
    
        // verifica se o Sexo está correto
        if (dto.SexoCliente != 'M' && dto.SexoCliente != 'F')
        {
            return Results.BadRequest("O sexo não é nem M ou F.");
        }

        // verifica se o Cpf está no formato correto
        if (!string.IsNullOrWhiteSpace(dto.CpfCliente))
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.CpfCliente, "^[0-9]{11}$", RegexOptions.None, TimeSpan.FromMilliseconds(100)))
            {
                return Results.BadRequest("CPF no formato inválido.");
            }
        
            // verifica se o CPF já existe usando CountAsync
            var cpfExistente = await _db.Clientes
                .Where(c => c.CpfCliente == dto.CpfCliente
                            && (!ignoreId.HasValue || c.ClienteId != ignoreId.Value))
                .CountAsync() > 0;
        
            if (cpfExistente)
                return Results.Conflict($"Já existe um cliente para esse CPF.");
        }

        return null;
    }

    
}