using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class CargoReadResponseExample : IExamplesProvider<ResourceResponse<CargoReadDto>>
{
    public ResourceResponse<CargoReadDto> GetExamples()
    {
        var cargo = new CargoReadDto(
            1,
            "Auxiliar",
            "Responsável por auxiliar nas tarefas gerais da empresa"
        );

        var links = new List<LinkDto>
        {
            new LinkDto("/cargos/1", "self", "GET"),
            new LinkDto("/cargos", "list", "GET")
        };

        return new ResourceResponse<CargoReadDto>(cargo, links);
    }
}