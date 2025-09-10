using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class PatioEndpoints
{
    public static void MapPatioEndpoints(this IEndpointRouteBuilder app)
    {
        var patios = app.MapGroup("/patios").WithTags("Patios");
        
        patios.MapGet("/", async ([Description("O número da página atual")]int pageNumber, [Description("A quantidade de registros por página")] int pageSize, PatioService service) => await service.GetAllPatiosAsync(pageNumber, pageSize))
            .WithSummary("Retorna a lista de pátios com setores e vagas")
            .WithDescription("Retorna todos os pátios cadastrados, com seus respectivos setores e vagas.")
            .Produces<PagedResponse<PatioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        patios.MapGet("/{id:int}", async ([Description("Identificador único de Pátio")] int id, PatioService service) => await service.GetPatioByIdAsync(id))
            .WithSummary("Retorna um patio pelo ID")
            .WithDescription("Retorna um patio a partir de um ID. Retorna 200 OK se o patio for encontrado, ou erro se não for achado.")
            .Produces<ResourceResponse<PatioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}