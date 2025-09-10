using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class MovimentacaoEndpoints
{
    public static void MapMovimentacaoEndpoints(this IEndpointRouteBuilder app)
    {
        var movimentacoes = app.MapGroup("/movimentacoes").WithTags("Movimentações");

        movimentacoes.MapGet("/", async ([Description("O número da página atual")]int pageNumber, [Description("A quantidade de registros por página")] int pageSize, MovimentacaoService service) => await service.GetAllMovimentacoesAsync(pageNumber, pageSize))
            .WithSummary("Retorna a lista de movimentações")
            .WithDescription("Retorna a lista de movimentações feitas, com dados da moto, cliente e vaga.")
            .Produces<PagedResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/{id:int}", async ([Description("Identificador único de Movimentação")] int id, MovimentacaoService service) => await service.GetMovimentacaoByIdAsync(id))
            .WithSummary("Retorna uma movimentação pelo ID")
            .WithDescription("Retorna uma movimentação a partir de um ID. Retorna 200 OK se a movimentação for encontrada, ou erro se não for achada.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/por-moto/{motoId}", async ([Description("Identificador único de Moto")] int motoId, [Description("O número da página atual")]int pageNumber, [Description("A quantidade de registros por página")] int pageSize, MovimentacaoService service) => await service.GetMovimentacoesByMotoIdAsync(motoId, pageNumber, pageSize))
            .WithSummary("Retorna movimentações de uma moto específica")
            .WithDescription("Retorna a lista de movimentações associadas a uma moto.")
            .Produces<PagedResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapGet("/ocupacao-por-setor/patio/{id}", async ([Description("Identificador único de Pátio")] int id, [Description("O número da página atual")]int pageNumber, [Description("A quantidade de registros por página")] int pageSize, MovimentacaoService service) => await service.GetTotalVagasOcupadasPatioAsync(id, pageNumber, pageSize))
            .WithSummary("Retorna o total de vagas por setor")
            .WithDescription("Retorna o total de vagas e o total de vagas ocupadas por setor a partir do ID de um pátio.")
            .Produces<PagedResponse<VagasSetorDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapPost("/", async (MovimentacaoPostDto dto, MovimentacaoService service) => await service.CreateMovimentacaoAsync(dto))
            .Accepts<MovimentacaoPostDto>("application/json")
            .WithSummary("Cria uma nova movimentação")
            .WithDescription("Cria uma nova movimentação no sistema, atualizando o status da moto e o status da vaga.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status409Conflict)
            .Produces(StatusCodes.Status500InternalServerError);
        
        movimentacoes.MapPut("/{id:int}/saida", async ([Description("Identificador único de Movimentação")] int id, MovimentacaoService service) => await service.UpdateMovimentacaoAsync(id))
            .WithSummary("Atualiza a data de saída da movimentação.")
            .WithDescription("Altera a data de saída de uma movimentação, finalizando-a. Atualiza a situação da moto para 'Em Trânsito' e desocupa a vaga.")
            .Produces<ResourceResponse<MovimentacaoReadDto>>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }   
}