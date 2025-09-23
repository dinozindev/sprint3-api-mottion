using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class VagaReadResponseExample : IExamplesProvider<ResourceResponse<VagaReadDto>>
{
    public ResourceResponse<VagaReadDto> GetExamples()
    {
        var vaga = new VagaReadDto(
            1,
            "A1-V1",
            0,
            new SetorResumoDto(
                1,
                "Pendência",
                "Parcial",
                1
            ));
    
        var links = new List<LinkDto>
        {
            new LinkDto("/vagas/1", "self", "GET"),
            new LinkDto("/vagas", "list", "GET")
        };

        return new ResourceResponse<VagaReadDto>(vaga, links);
    }
}