using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MovimentacaoReadResponseExample : IExamplesProvider<ResourceResponse<MovimentacaoReadDto>>
{
    public ResourceResponse<MovimentacaoReadDto> GetExamples()
    {
        var movimentacao = new MovimentacaoReadDto(
            1,
            DateTime.Today,
            DateTime.Today,
            "Movimentação da Moto de ID 1 para a Vaga de ID 1",
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
                    "Parcial",
                    1
                )
            )
            );

        var links = new List<LinkDto>
        {
            new LinkDto("/movimentacoes/1", "self", "GET"),
            new LinkDto("/movimentacoes/1", "update", "PUT"),
            new LinkDto("/movimentacoes/1", "delete", "DELETE"),
            new LinkDto("/movimentacoes", "list", "GET")
        };

        return new ResourceResponse<MovimentacaoReadDto>(movimentacao, links);
    }
}