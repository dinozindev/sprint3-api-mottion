using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class OcupacaoSetorReadExample : IExamplesProvider<PagedResponse<VagasSetorDto>>
{
    public PagedResponse<VagasSetorDto> GetExamples()
    {
        var ocupacoes = new List<VagasSetorDto>
        {
         new VagasSetorDto(
            "Pendência",
            4,
            2
            ),
         new VagasSetorDto(
            "Reparos Simples",
            4,
            2
            ),
        new VagasSetorDto(
            "Danos Estruturais Graves",
            4,
            2
            )
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/movimentacoes/ocupacao-por-setor/patio/1?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("create", "/movimentacoes", "POST"),
            new LinkDto("next", "/movimentacoes/ocupacao-por-setor/patio/1?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<VagasSetorDto>(
            TotalCount: ocupacoes.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: ocupacoes,
            Links: links
        );
    }
}