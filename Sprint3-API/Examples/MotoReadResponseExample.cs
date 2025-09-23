using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MotoReadResponseExample : IExamplesProvider<ResourceResponse<MotoReadDto>>
{
    public ResourceResponse<MotoReadDto> GetExamples()
    {
        var moto = new MotoReadDto(
            1,
            "ABC1D25",
            "Mottu Pop",
            "Ativa",
            "CHS12345678901238",
            null
            );

        var links = new List<LinkDto>
        {
            new LinkDto("/motos/1", "self", "GET"),
            new LinkDto("/motos/1", "update", "PUT"),
            new LinkDto("/motos/1", "delete", "DELETE"),
            new LinkDto("/motos", "list", "GET")
        };

        return new ResourceResponse<MotoReadDto>(moto, links);
    }
}