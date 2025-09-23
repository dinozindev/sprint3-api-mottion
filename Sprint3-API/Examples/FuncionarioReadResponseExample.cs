using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class FuncionarioReadResponseExample : IExamplesProvider<ResourceResponse<FuncionarioReadDto>>
{
    public ResourceResponse<FuncionarioReadDto> GetExamples()
    {
        var funcionario = new FuncionarioReadDto(
            1, "Ricardo Ramos", "11911112222", new CargoReadDto(1, "Auxiliar", "Responsável por auxiliar nas tarefas gerais da empresa"), new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta")
        );

        var links = new List<LinkDto>
        {
            new LinkDto("/funcionarios/1", "self", "GET"),
            new LinkDto("/funcionarios", "list", "GET")
        };

        return new ResourceResponse<FuncionarioReadDto>(funcionario, links);
    }
}