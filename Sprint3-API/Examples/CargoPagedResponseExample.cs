using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class CargoPagedResponseExample : IExamplesProvider<PagedResponse<CargoReadDto>>
{
    public PagedResponse<CargoReadDto> GetExamples()
    {
        var cargos = new List<CargoReadDto>
        {
            new CargoReadDto(1, "Auxiliar", "Responsável por auxiliar nas tarefas gerais da empresa"),
            new CargoReadDto(2, "Gerente", "Gerencia processos e pessoas"),
            new CargoReadDto(3, "Mecânico", "Executa reparos e manutenção")
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/cargos?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/cargos?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<CargoReadDto>(
            TotalCount: cargos.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: cargos,
            Links: links
        );
    }
}