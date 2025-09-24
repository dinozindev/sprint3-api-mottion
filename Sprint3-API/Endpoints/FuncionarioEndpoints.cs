using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class FuncionarioEndpoints
{
    public static void MapFuncionarioEndpoints(this IEndpointRouteBuilder app)
    {
        var funcionarios = app.MapGroup("funcionarios").WithTags("Funcionários");
        
        funcionarios.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, FuncionarioService service) => await service.GetAllFuncionariosAsync(pageNumber, pageSize))
            .WithSummary("Retorna todos os funcionários cadastrados (paginação)")
            .WithDescription("Este endpoint retorna a lista de funcionários cadastrados, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<FuncionarioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        funcionarios.MapGet("/{id:int}", async ([Description("Identificador único de Funcionário")] int id, FuncionarioService service) => await service.GetFuncionarioByIdAsync(id))
            .WithSummary("Retorna um funcionário pelo ID")
            .WithDescription("Este endpoint retorna os dados de um funcionário a partir do seu ID. Retorna 200 OK se o funcionário for encontrado, ou 404 Not Found se não for achado.")
            .Produces<ResourceResponse<FuncionarioReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        }
}
