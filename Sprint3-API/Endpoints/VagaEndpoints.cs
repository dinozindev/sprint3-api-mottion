using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class VagaEndpoints
{
    public static void MapVagaEndpoints(this IEndpointRouteBuilder app)
    {
        var vagas = app.MapGroup("/vagas").WithTags("Vagas");
        
        vagas.MapGet("/", async (int pageNumber, int pageSize, VagaService service) => await service.GetAllVagasAsync(pageNumber, pageSize))
            .WithSummary("Retorna a lista de vagas")
            .WithDescription("Retorna a lista de vagas cadastradas.")
            .Produces<PagedResponse<VagaReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        vagas.MapGet("/{id:int}", async ([Description("Identificador único de Vaga")] int id, VagaService service) => await service.GetVagaByIdAsync(id))
            .WithSummary("Retorna uma vaga pelo ID")
            .WithDescription("Retorna uma vaga a partir de um ID. Retorna 200 OK se a vaga for encontrada, ou erro se não for achada.")
            .Produces<ResourceResponse<VagaReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
