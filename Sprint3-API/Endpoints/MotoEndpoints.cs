using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;
using Swashbuckle.AspNetCore.Filters;
namespace Sprint3_API.Endpoints;

public static class MotoEndpoints
{
    public static void MapMotoEndpoints(this IEndpointRouteBuilder app)
    {
        var motos = app.MapGroup("/motos").WithTags("Motos");
        
        motos.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, MotoService service) => await service.GetAllMotosAsync(pageNumber, pageSize))
            .WithSummary("Retorna todas as motos cadastradas (paginação)")
            .WithDescription("Este endpoint retorna a lista de motos cadastradas, paginadas de acordo com os parâmetros **pageNumber e pageSize**. O cliente pode ser nulo ou não.")
            .Produces<PagedResponse<MotoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);

        motos.MapGet("/{id:int}",
                async ([Description("Identificador único de Moto")] int id, MotoService service) =>
                await service.GetMotoByIdAsync(id))
            .WithSummary("Retorna uma moto pelo ID")
            .WithDescription(
                "Este endpoint retorna os dados de uma moto a partir do seu ID. Retorna 200 OK se a moto for encontrada, ou 404 Not Found se não for achada.")
            .Produces<ResourceResponse<MotoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        motos.MapGet("/por-chassi/{numeroChassi}", async ([Description("Número de Chassi único da Moto")] string numeroChassi, MotoService service) => await service.GetMotoByChassiAsync(numeroChassi))
            .WithSummary("Retorna uma moto pelo número de chassi")
            .WithDescription("Este endpoint retorna os dados de uma moto a partir do seu número de Chassi. Retorna 200 OK se a moto for encontrada, ou 404 Not Found se não for achada.")
            .Produces<ResourceResponse<MotoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        motos.MapGet("/{id:int}/ultima-posicao", async ([Description("Identificador único de Moto")] int id, MotoService service) => await service.GetMotoUltimaPosicaoAsync(id))
            .WithSummary("Retorna a última posição de uma moto")
            .WithDescription("Este endpoint retorna a última vaga e setor em que uma moto esteve a partir de seu ID, com base na movimentação mais recente.")
            .Produces<ResourceResponse<UltimaPosicaoDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        motos.MapPost("/", async (MotoPostDto dto, MotoService service) => await service.CreateMotoAsync(dto))
            .Accepts<MotoPostDto>("application/json")
            .WithSummary("Cria uma moto")
            .WithDescription("Este endpoint cria uma moto a partir da placa, modelo, situação e número de chassi. Retorna 201 Created se a moto for criada com sucesso, ou erro caso não seja possível.")
            .Produces<ResourceResponse<MotoReadDto>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        motos.MapPut("/{id:int}", async ([Description("Identificador único de Moto")] int id, MotoPostDto dto, MotoService service) => await service.UpdateMotoAsync(id, dto))
            .Accepts<MotoPostDto>("application/json")
            .WithSummary("Atualiza uma moto")
            .WithDescription("Este endpoint é responsável por atualizar os dados existentes de uma moto a partir de seu ID. Retorna 200 OK se a moto for atualizada com sucesso, ou erro caso não seja possível.")
            .Produces<ResourceResponse<MotoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        motos.MapDelete("/{id:int}", async ([Description("Identificador único de Moto")] int id, MotoService service) => await service.DeleteMotoAsync(id))
            .WithSummary("Deleta uma moto pelo ID")
            .WithDescription("Este endpoint é responsável por deletar uma moto pelo ID informado. Retorna 204 No Content se a moto for deletada com sucesso, ou erro se não for achada.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        motos.MapPut("/{id:int}/remover-cliente", async ([Description("Identificador único de Moto")] int id, MotoService service) => await service.DeleteAssociacaoClienteMotoAsync(id))
            .WithSummary("Remove a associação do cliente a moto")
            .WithDescription("Este endpoint é responsável por remover a associação do cliente de uma moto através do ID da moto. Retorna 204 No Content se a remoção for feita com sucesso, ou 404 Not Found se a moto não for achada.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        
        motos.MapPut("/{id:int}/alterar-cliente/{clienteId}", async ([Description("Identificador único de Moto")] int id, [Description("Identificador único de Cliente")] int clienteId, MotoService service) => await service.UpdateAssociacaoClienteMotoAsync(id, clienteId))
            .WithSummary("Altera a associação do cliente a moto")
            .WithDescription("Este endpoint é responsável por alterar a associação do cliente de uma moto através do ID da moto e do cliente. Retorna 204 se a alteração for feita com sucesso, ou 404 Not Found se um dos dois valores não for encontrado.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }    
}