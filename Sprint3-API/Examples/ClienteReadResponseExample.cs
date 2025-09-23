using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class ClienteReadResponseExample : IExamplesProvider<ResourceResponse<ClienteReadDto>>
{
    public ResourceResponse<ClienteReadDto> GetExamples()
    {
        var cliente = new ClienteReadDto(
            1,
            "Marcos Dos Santos",
            "11948372632",
            'M',
            "mdsantos@gmail.com",
            "43221254321",
            new List<MotoResumoDto>()
            );

        var links = new List<LinkDto>
        {
            new LinkDto("/clientes/1", "self", "GET"),
            new LinkDto("/clientes/1", "update", "PUT"),
            new LinkDto("/clientes/1", "delete", "DELETE"),
            new LinkDto("/clientes", "list", "GET")
        };

        return new ResourceResponse<ClienteReadDto>(cliente, links);
    }
}