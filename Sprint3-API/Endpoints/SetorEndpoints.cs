using System.ComponentModel;
using Sprint3_API.Dtos;
using Sprint3_API.Services;

namespace Sprint3_API.Endpoints;

public static class SetorEndpoints
{
   public static void MapSetorEndpoints(this IEndpointRouteBuilder app)
   {
      var setores = app.MapGroup("/setores").WithTags("Setores");

      setores.MapGet("/", async ([Description("O número da página atual (ex: 1)")]int pageNumber, [Description("A quantidade de registros por página (ex: 10)")] int pageSize, SetorService service) => await service.GetAllSetoresAsync(pageNumber, pageSize))
         .WithSummary("Retorna todos os setores cadastrados (paginação)")
         .WithDescription("Este endpoint retorna a lista de setores cadastrados, paginados de acordo com os parâmetros **pageNumber** e **pageSize**.")
         .Produces<PagedResponse<SetorReadDto>>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status204NoContent)
         .Produces(StatusCodes.Status500InternalServerError);
      
      setores.MapGet("/{id:int}", async ([Description("Identificador único de Setor")] int id, SetorService service) => await service.GetSetorByIdAsync(id))
         .WithSummary("Retorna um setor pelo ID")
         .WithDescription("Este endpoint retorna os dados de um setor a partir do seu ID. Retorna 200 OK se o setor for encontrado, ou 404 Not Found se não for achado.")
         .Produces<ResourceResponse<SetorReadDto>>(StatusCodes.Status200OK)
         .Produces(StatusCodes.Status404NotFound)
         .Produces(StatusCodes.Status500InternalServerError);
   } 
}