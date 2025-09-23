using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class VagaPagedResponseExample : IExamplesProvider<PagedResponse<VagaReadDto>>
{
    public PagedResponse<VagaReadDto> GetExamples()
    {
        var Vagas = new List<VagaReadDto>
        {
            new VagaReadDto(
            1,
            "A1-V1",
            0,
            new SetorResumoDto(
                1,
                "Pendência",
                "Parcial",
                1
            )),
            new VagaReadDto(
            2,
            "A1-V2",
            0,
            new SetorResumoDto(
                1,
                "Pendência",
                "Parcial",
                1
            )),
            new VagaReadDto(
            3,
            "A1-V3",
            1,
            new SetorResumoDto(
                1,
                "Pendência",
                "Parcial",
                1
            )),
            new VagaReadDto(
            4,
            "A1-V4",
            1,
            new SetorResumoDto(
                1,
                "Pendência",
                "Parcial",
                1
            ))
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/vagas?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/vagas?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<VagaReadDto>(
            TotalCount: Vagas.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: Vagas,
            Links: links
        );
    }
}