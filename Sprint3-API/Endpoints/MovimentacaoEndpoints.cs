using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class MovimentacaoEndpoints
{
    public static void MapMovimentacaoEndpoints(this IEndpointRouteBuilder app)
    {
        var movimentacoes = app.MapGroup("/movimentacoes").WithTags("Movimentações");

        movimentacoes.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, MovimentacaoService service) => await service.GetAllMovimentacoesAsync(pageNumber, pageSize))
            .WithSummary("Retorna todas as movimentações cadastradas (paginação)")
            .WithDescription("Este endpoint retorna a lista de movimentações cadastradas, com dados da moto, cliente e vaga, paginadas de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/{id:int}", async ([Description("Identificador único de Movimentação")] int id, MovimentacaoService service) => await service.GetMovimentacaoByIdAsync(id))
            .WithSummary("Retorna uma movimentação pelo ID")
            .WithDescription("Este endpoint retorna os dados de uma movimentação a partir do seu ID. Retorna 200 OK se a movimentação for encontrada, ou 404 Not Found se não for achada.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/por-moto/{motoId}", async ([Description("Identificador único de Moto")] int motoId, [Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, MovimentacaoService service) => await service.GetMovimentacoesByMotoIdAsync(motoId, pageNumber, pageSize))
            .WithSummary("Retorna todas as movimentações de uma moto específica (paginação)")
            .WithDescription("Este endpoint retorna a lista de movimentações associadas a uma moto a partir do seu ID, paginadas de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/ocupacao-por-setor/patio/{id}", async ([Description("Identificador único de Pátio")] int id, [Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, MovimentacaoService service) => await service.GetTotalVagasOcupadasPatioAsync(id, pageNumber, pageSize))
            .WithSummary("Retorna a ocupação de vagas por setor de um pátio (paginação)")
            .WithDescription("Este endpoint retorna o total de vagas e o total de vagas ocupadas por setor a partir do ID de um pátio, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
            .Produces<PagedResponse<VagasSetorDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapPost("/", async (MovimentacaoPostDto dto, MovimentacaoService service) => await service.CreateMovimentacaoAsync(dto))
            .Accepts<MovimentacaoPostDto>("application/json")
            .WithSummary("Cria uma movimentação")
            .WithDescription("Este endpoint cria uma movimentação no sistema a partir de uma descrição, ID da moto e ID da vaga, atualizando o status da moto e o status da vaga. Retorna 201 Created se a movimentação for criada com sucesso, ou erro caso não seja possível.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapPut("/{id:int}/saida", async ([Description("Identificador único de Movimentação")] int id, MovimentacaoService service) => await service.UpdateMovimentacaoAsync(id))
            .WithSummary("Atualiza a data de saída de uma movimentação")
            .WithDescription("Este endpoint altera a data de saída de uma movimentação, finalizando-a. Atualiza a situação da moto para 'Em Trânsito' e desocupa a vaga. Retorna 200 OK se a movimentação for atualizada, ou erro caso não seja possível.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapDelete("/{id:int}", async ([Description("Identificador único de Movimentação")] int id, MovimentacaoService service) => await service.DeleteMovimentacaoAsync(id))
            .WithSummary("Deleta uma movimentação pelo ID")
            .WithDescription("Este endpoint é responsável por deletar uma movimentação pelo ID informado. Retorna 204 No Content se a movimentação for deletada com sucesso, ou 404 Not Found se não for achada.")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
    }   
}