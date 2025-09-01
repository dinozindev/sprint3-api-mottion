using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class CargoEndpoints
{
    public static void MapCargoEndpoints(this IEndpointRouteBuilder app)
    {
        var cargos = app.MapGroup("/cargos").WithTags("Cargos");
        
        cargos.MapGet("/", async (int pageNumber, int pageSize, CargoService service) => await service.GetAllCargosAsync(pageNumber, pageSize))
            .WithSummary("Retorna a lista de cargos.")
            .WithDescription("Retorna a lista de cargos cadastrados.")
            .Produces<PagedResponse<CargoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        cargos.MapGet("/{id:int}", async ([Description("Identificador único de Cargo")] int id, CargoService service) => await service.GetCargoByIdAsync(id))
            .WithSummary("Retorna um cargo pelo ID")
            .WithDescription("Retorna um cargo a partir de um ID. Retorna 200 OK se o cargo for encontrado, ou erro se não for achado.")
            .Produces<ResourceResponse<CargoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}