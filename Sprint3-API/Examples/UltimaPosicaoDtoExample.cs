using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class UltimaPosicaoDtoExample : IExamplesProvider<ResourceResponse<UltimaPosicaoDto>>
{
    public ResourceResponse<UltimaPosicaoDto> GetExamples()
    {
        var ultimaPosicao = new UltimaPosicaoDto(
            new VagaResumoDto(1, "A1-V1", 0),
            new SetorResumoDto(1, "Pendência", "Parcial", 1),
            DateTime.Today,
            DateTime.Today,
            false
            );

        var links = new List<LinkDto>
        {
            new LinkDto("/motos/1", "self", "GET"),
            new LinkDto("/motos/1", "update", "PUT"),
            new LinkDto("/motos/1", "delete", "DELETE"),
            new LinkDto("/motos", "list", "GET")
        };

        return new ResourceResponse<UltimaPosicaoDto>(ultimaPosicao, links);
    }
}