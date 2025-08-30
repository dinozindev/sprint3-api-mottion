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
        
        clientes.MapGet("/", async (ClienteService service) => await service.GetAllClientesAsync())
            .WithSummary("Retorna a lista de todos os clientes.")
            .WithDescription("Retorna a lista de todos os clientes cadastrados no sistema, incluindo também motos associadas a cada um.")
            .Produces<List<ClienteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapGet("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClienteService service) => await service.GetClienteByIdAsync(id))
            .WithSummary("Retorna um cliente com sua(s) moto(s) pelo ID")
            .WithDescription("Retorna um cliente e suas motos associadas (caso existam) pelo ID. Retorna 200 OK se o cliente for encontrado, ou erro se não for achado.")
            .Produces<ClienteReadDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        clientes.MapPost("/",
                async (ClientePostDto dto, ClienteService service) => await service.CreateClienteAsync(dto))
            .WithSummary("Cria um cliente")
            .WithDescription("Cria um cliente no sistema.")
            .Produces<Cliente>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapPut("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClientePostDto dto, ClienteService service) => await service.UpdateClienteAsync(id, dto)) 
            .WithSummary("Atualiza um cliente")
            .WithDescription("Atualiza os dados de um cliente já existente.")
            .Produces<Moto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        clientes.MapDelete("/{id:int}", async ([Description("Identificador único de Cliente")] int id, ClienteService service) => await service.DeleteClienteAsync(id))
            .WithSummary("Deleta um cliente pelo ID")
            .WithDescription("Deleta um cliente pelo ID informado. Retorna 204 No Content caso encontrado, ou erro se não achado.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}