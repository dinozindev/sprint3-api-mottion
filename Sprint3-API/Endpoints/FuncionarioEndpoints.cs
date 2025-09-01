using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class FuncionarioEndpoints
{
    public static void MapFuncionarioEndpoints(this IEndpointRouteBuilder app)
    {
        var funcionarios = app.MapGroup("funcionarios").WithTags("Funcionários");
        
        funcionarios.MapGet("/", async (int pageNumber, int pageSize, FuncionarioService service) => await service.GetAllFuncionariosAsync(pageNumber, pageSize))
            .WithSummary("Retorna a lista de funcionários")
            .WithDescription("Retorna a lista de funcionários cadastrados.")
            .Produces<PagedResponse<FuncionarioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        funcionarios.MapGet("/{id:int}", async ([Description("Identificador único de Funcionário")] int id, FuncionarioService service) => await service.GetFuncionarioByIdAsync(id))
            .WithSummary("Retorna um funcionário pelo ID")
            .WithDescription("Retorna um funcionário a partir de um ID. Retorna 200 OK se o funcionário for encontrado, ou erro se não for achado.")
            .Produces<ResourceResponse<FuncionarioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
}
