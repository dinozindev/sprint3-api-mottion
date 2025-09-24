using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class PatioEndpoints
{
    public static void MapPatioEndpoints(this IEndpointRouteBuilder app)
    {
        var patios = app.MapGroup("/patios").WithTags("Patios");
        
        patios.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, PatioService service) => await service.GetAllPatiosAsync(pageNumber, pageSize))
            .WithSummary("Retorna todos os pátios cadastrados (paginação)")
            .WithDescription("Este endpoint retorna a lista de pátios cadastrados, com seus respectivos setores e vagas, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<PatioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        patios.MapGet("/{id:int}", async ([Description("Identificador único de Pátio")] int id, PatioService service) => await service.GetPatioByIdAsync(id))
            .WithSummary("Retorna um pátio pelo ID")
            .WithDescription("Este endpoint retorna os dados de um pátio a partir do seu ID. Retorna 200 OK se o pátio for encontrado, ou 404 Not Found se não for achado.")
            .Produces<ResourceResponse<PatioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}