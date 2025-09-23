using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class FuncionarioPagedResponseExample : IExamplesProvider<PagedResponse<FuncionarioReadDto>>
{
    public PagedResponse<FuncionarioReadDto> GetExamples()
    {
        var funcionarios = new List<FuncionarioReadDto>
        {
            new FuncionarioReadDto(1, "Ricardo Ramos", "11911112222", new CargoReadDto(1, "Auxiliar", "Responsável por auxiliar nas tarefas gerais da empresa"), new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta"));
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/funcionarios?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/funcionarios?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<FuncionarioReadDto>(
            TotalCount: funcionarios.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: funcionarios,
            Links: links
        );
    }
}