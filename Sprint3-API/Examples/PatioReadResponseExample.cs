using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class PatioReadResponseExample : IExamplesProvider<ResourceResponse<PatioReadDto>>
{
    public ResourceResponse<PatioReadDto> GetExamples()
    {
        var patio = new PatioReadDto(
            1,
            "Zona Norte",
            "Pátio Norte",
            "Área ampla e coberta",
            new List<SetorResumoPatioDto>{
                new SetorResumoPatioDto(
                    1,
                    "Pendência",
                    "Parcial",
                    new List<VagaResumoDto>{
                        new VagaResumoDto(1, "A1-V1", 1),
                        new VagaResumoDto(2, "A1-V2", 0),
                        new VagaResumoDto(3, "A1-V3", 1),
                        new VagaResumoDto(4, "A1-V4", 0)
                    })
            }
        );

        var links = new List<LinkDto>
        {
            new LinkDto("/patios/1", "self", "GET"),
            new LinkDto("/patios", "list", "GET")
        };

        return new ResourceResponse<PatioReadDto>(patio, links);
    }
}