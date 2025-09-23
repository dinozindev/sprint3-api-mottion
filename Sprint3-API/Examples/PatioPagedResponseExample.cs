using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class PatioPagedResponseExample : IExamplesProvider<PagedResponse<PatioReadDto>>
{
    public PagedResponse<PatioReadDto> GetExamples()
    {
        var patios = new List<PatioReadDto>
        {
            new PatioReadDto(
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
            ),
            new PatioReadDto(
            2,
            "Zona Sul",
            "Pátio Sul",
            "Coberto Parcialmente",
            new List<SetorResumoPatioDto>{
                new SetorResumoPatioDto(
                    9,
                    "Pendência",
                    "Livre",
                    new List<VagaResumoDto>{
                        new VagaResumoDto(33, "B1-V1", 0),
                        new VagaResumoDto(34, "B1-V2", 0),
                        new VagaResumoDto(35, "B1-V3", 0),
                        new VagaResumoDto(36, "B1-V4", 0)
                    })
                }
            ),

        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/patios?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/patios?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<PatioReadDto>(
            TotalCount: patios.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: patios,
            Links: links
        );
    }
}