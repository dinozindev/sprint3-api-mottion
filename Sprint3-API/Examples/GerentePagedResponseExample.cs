using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class GerentePagedResponseExample : IExamplesProvider<PagedResponse<GerenteReadDto>>
{
    public PagedResponse<GerenteReadDto> GetExamples()
    {
        var gerentes = new List<GerenteReadDto>
        {
            new GerenteReadDto(1, "Rodrigo Neves", "11900001111", "99999999900", new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta")),
            new GerenteReadDto(2, "Carla Pires", "11900002222", "88888888801", new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta")),
            new GerenteReadDto(3, "Fernando Lopes", "11900003333", "77777777702", new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta"))
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/gerentes?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("next", "/gerentes?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<GerenteReadDto>(
            TotalCount: gerentes.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: gerentes,
            Links: links
        );
    }
}