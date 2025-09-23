using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class SetorReadResponseExample : IExamplesProvider<ResourceResponse<SetorReadDto>>
{
    public ResourceResponse<SetorReadDto> GetExamples()
    {
        var setor = new SetorReadDto(
            1,
            "Pendência",
            "Parcial",
            new PatioResumoDto(
                1, "Zona Norte", "Pátio Norte", "Área ampla e coberta"
            ),
            new List<VagaResumoDto>{
                new VagaResumoDto(1, "A1-V1", 1),
                new VagaResumoDto(2, "A1-V2", 0),
                new VagaResumoDto(3, "A1-V3", 1),
                new VagaResumoDto(4, "A1-V4", 0)
                });
    
        var links = new List<LinkDto>
        {
            new LinkDto("/setores/1", "self", "GET"),
            new LinkDto("/setores", "list", "GET")
        };

        return new ResourceResponse<SetorReadDto>(setor, links);
    }
}