using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class SetorPagedResponseExample : IExamplesProvider<PagedResponse<SetorReadDto>>
{
    public PagedResponse<SetorReadDto> GetExamples()
    {
        var setores = new List<SetorReadDto>
        {
            new SetorReadDto(
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
                    }),
            new SetorReadDto(
                2,
                "Reparos Simples",
                "Parcial",
                new PatioResumoDto(
                    1, "Zona Norte", "Pátio Norte", "Área ampla e coberta"
                ),
                new List<VagaResumoDto>{
                    new VagaResumoDto(5, "A2-V1", 1),
                    new VagaResumoDto(6, "A2-V2", 0),
                    new VagaResumoDto(7, "A2-V3", 1),
                    new VagaResumoDto(8, "A2-V4", 0)
                    }),
            new SetorReadDto(
                3,
                "Danos Estruturais Graves",
                "Parcial",
                new PatioResumoDto(
                    1, "Zona Norte", "Pátio Norte", "Área ampla e coberta"
                ),
                new List<VagaResumoDto>{
                    new VagaResumoDto(9, "A3-V1", 1),
                    new VagaResumoDto(10, "A3-V2", 0),
                    new VagaResumoDto(11, "A3-V3", 1),
                    new VagaResumoDto(12, "A3-V4", 0)
                    })
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/setores?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/setores?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<SetorReadDto>(
            TotalCount: setores.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: setores,
            Links: links
        );
    }
}