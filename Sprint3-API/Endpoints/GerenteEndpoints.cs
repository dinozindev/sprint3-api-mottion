using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class GerenteEndpoints
{
    public static void MapGerenteEndpoints(this IEndpointRouteBuilder app)
    {
        var gerentes = app.MapGroup("/gerentes").WithTags("Gerentes");
        
        gerentes.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, GerenteService service) => await service.GetAllGerentesAsync(pageNumber, pageSize))
            .WithSummary("Retorna todos os gerentes cadastrados (paginação)")
            .WithDescription("Este endpoint retorna a lista de gerentes cadastrados, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<GerenteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        gerentes.MapGet("/{id:int}", async ([Description("Identificador único de Gerente")] int id, GerenteService service) => await service.GetGerenteByIdAsync(id))
            .WithSummary("Retorna um gerente pelo ID")
            .WithDescription("Este endpoint retorna os dados de um gerente a partir do seu ID. Retorna 200 OK se o gerente for encontrado, ou 404 Not Found se não for achado.")
            .Produces<ResourceResponse<GerenteReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}