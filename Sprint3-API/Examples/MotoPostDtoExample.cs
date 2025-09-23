using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MotoPostDtoExample : IExamplesProvider<MotoPostDto>
{
    public MotoPostDto GetExamples()
    {
        return new MotoPostDto(
            "ABC1D25",
            "Mottu Pop",
            "Ativa",
            "CHS12345678901238"
        );
    }
}