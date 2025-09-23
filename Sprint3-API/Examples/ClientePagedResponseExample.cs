using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class ClientePagedResponseExample : IExamplesProvider<PagedResponse<ClienteReadDto>>
{
    public PagedResponse<ClienteReadDto> GetExamples()
    {
        var clientes = new List<ClienteReadDto>
        {
            new ClienteReadDto(
                1, 
                "11912345678", 
                "Carlos Silva", 
                'M', 
                "carlos@email.com", 
                "12345678900", 
                new List<MotoResumoDto>
                {
                    new MotoResumoDto(1, "ABC1D25", "Mottu Pop", "Ativa", "CHS12345678901238")
                }),
            new ClienteReadDto(2, "11987654321", "Maria Souza", 'F', "maria@email.com", "23456789011", new List<MotoResumoDto>()),
            new ClienteReadDto(3, "1188887777", "João Mendes", 'M', "joao@email.com", "34567890122", new List<MotoResumoDto>())
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/clientes?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("create", "/clientes", "POST"),
            new LinkDto("next", "/clientes?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<ClienteReadDto>(
            TotalCount: clientes.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: clientes,
            Links: links
        );
    }
}