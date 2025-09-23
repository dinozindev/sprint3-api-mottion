using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MovimentacaoPostDtoExample : IExamplesProvider<MovimentacaoPostDto>
{
    public MovimentacaoPostDto GetExamples()
    {
        return new MovimentacaoPostDto(
            "Movimentação da Moto de ID 1 para a Vaga de ID 1",
            1,
            1
        );
    }
}