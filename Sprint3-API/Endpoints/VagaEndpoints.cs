using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class VagaEndpoints
{
    public static void MapVagaEndpoints(this IEndpointRouteBuilder app)
    {
        var vagas = app.MapGroup("/vagas").WithTags("Vagas");
        
        vagas.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, VagaService service) => await service.GetAllVagasAsync(pageNumber, pageSize))
            .WithSummary("Retorna todas as vagas cadastradas (paginação)")
            .WithDescription("Este endpoint retorna a lista de vagas cadastradas, paginadas de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<VagaReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        vagas.MapGet("/{id:int}", async ([Description("Identificador único de Vaga")] int id, VagaService service) => await service.GetVagaByIdAsync(id))
            .WithSummary("Retorna uma vaga pelo ID")
            .WithDescription("Este endpoint retorna os dados de uma vaga a partir do seu ID. Retorna 200 OK se a vaga for encontrada, ou 404 Not Found se não for achada.")
            .Produces<ResourceResponse<VagaReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
    }
