using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class CargoEndpoints
{
    public static void MapCargoEndpoints(this IEndpointRouteBuilder app)
    {
        var cargos = app.MapGroup("/cargos").WithTags("Cargos");
        
        cargos.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, CargoService service) => await service.GetAllCargosAsync(pageNumber, pageSize))
            .WithSummary("Retorna todos os cargos cadastrados (paginação)")
            .WithDescription("Este endpoint retorna a lista de cargos cadastrados, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<CargoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        cargos.MapGet("/{id:int}", async ([Description("Identificador único de Cargo")] int id, CargoService service) => await service.GetCargoByIdAsync(id))
            .WithSummary("Retorna um cargo pelo ID")
            .WithDescription("Este endpoint retorna os dados de um cargo a partir do seu ID. Retorna 200 OK se o cargo for encontrado, ou 404 Not Found se não for achado.")
            .Produces<ResourceResponse<CargoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}