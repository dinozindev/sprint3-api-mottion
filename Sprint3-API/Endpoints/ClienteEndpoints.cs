using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Models;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        var clientes = app.MapGroup("/clientes").WithTags("Clientes");
        
        clientes.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, ClienteService service) => await service.GetAllClientesAsync(pageNumber, pageSize))
            .WithSummary("Retorna todos os clientes cadastrados (paginação)")
            .WithDescription("Este endpoint retorna a lista de todos os clientes, paginados de acordo com os parâmetros **pageNumber** e **pageSize**, incluindo também as motos associadas a cada um (se existir).")
            .Produces<PagedResponse<ClienteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapGet("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClienteService service) => await service.GetClienteByIdAsync(id))
            .WithSummary("Retorna um cliente pelo ID")
            .WithDescription("Este endpoint retorna os dados de um cliente pelo ID, incluindo também as motos associadas a ele (se existir). Retorna 200 OK se o cliente for encontrado, ou 404 Not Found se não for achado.")
            .Produces<ResourceResponse<ClienteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        clientes.MapPost("/",
                async (ClientePostDto dto, ClienteService service) => await service.CreateClienteAsync(dto))
            .Accepts<ClientePostDto>("application/json")
            .WithSummary("Cria um cliente")
            .WithDescription("Este endpoint cria um cliente a partir do nome, telefone, sexo, email e CPF. Retorna 201 Created se o cliente for criado com sucesso, ou erro caso não seja possível.")
            .Produces<ResourceResponse<ClienteReadDto>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapPut("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClientePostDto dto, ClienteService service) => await service.UpdateClienteAsync(id, dto)) 
            .WithSummary("Atualiza um cliente")
            .WithDescription("Este endpoint é responsável por atualizar os dados existentes de um cliente a partir de seu ID. Retorna 200 OK se o cliente for atualizado com sucesso, ou erro caso não seja possível.")
            .Produces<ResourceResponse<ClienteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapDelete("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClienteService service) => await service.DeleteClienteAsync(id))
            .WithSummary("Deleta um cliente pelo ID")
            .WithDescription("Este endpoint é responsável por deletar um cliente pelo ID informado. Retorna 204 No Content se o cliente for deletado com sucesso, ou erro se não for achado.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}