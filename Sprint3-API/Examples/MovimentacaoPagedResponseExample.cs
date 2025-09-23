using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MovimentacaoPagedResponseExample : IExamplesProvider<PagedResponse<MovimentacaoReadDto>>
{
    public PagedResponse<MovimentacaoReadDto> GetExamples()
    {
        var movimentacoes = new List<MovimentacaoReadDto>
        {
         new MovimentacaoReadDto(
            1,
            DateTime.Today,
            DateTime.Today,
            "Aguardando liberação",
            new MotoReadDto(
                1,
                "ABC1234",
                "Mottu Pop",
                "Em Trânsito",
                "CHS12345678901234",
                new ClienteResumoDto(
                    1,
                    "Carlos Silva",
                    "11912345678",
                    'M',
                    "carlos@email.com",
                    "12345678900"
                )
            ),
            new VagaReadDto(
                1,
                "A1-V1",
                0,
                new SetorResumoDto(
                    1,
                    "Pendência",
                    "Paricial",
                    1
                )
            )
            )
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/movimentacoes?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("create", "/movimentacoes", "POST"),
            new LinkDto("next", "/movimentacoes?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<MovimentacaoReadDto>(
            TotalCount: movimentacoes.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: movimentacoes,
            Links: links
        );
    }
}