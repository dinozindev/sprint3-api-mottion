using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class GerenteReadResponseExample : IExamplesProvider<ResourceResponse<GerenteReadDto>>
{
    public ResourceResponse<GerenteReadDto> GetExamples()
    {
        var gerente = new GerenteReadDto(
            1, "Rodrigo Neves", "11900001111", "99999999900", new PatioResumoDto(1, "Zona Norte", "Pátio Norte", "Área ampla e coberta")
        );

        var links = new List<LinkDto>
        {
            new LinkDto("/gerentes/1", "self", "GET"),
            new LinkDto("/gerentes", "list", "GET")
        };

        return new ResourceResponse<GerenteReadDto>(gerente, links);
    }
}