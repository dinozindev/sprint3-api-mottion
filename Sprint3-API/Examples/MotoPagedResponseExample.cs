using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class MotoPagedResponseExample : IExamplesProvider<PagedResponse<MotoReadDto>>
{
    public PagedResponse<MotoReadDto> GetExamples()
    {
        var motos = new List<MotoReadDto>
        {
            new MotoReadDto(
            1,
            "ABC1234",
            "Mottu Pop",
            "Em Trânsito",
            "CHS12345678901234",
            new ClienteResumoDto(1, "Carlos Silva", "11912345678", 'M', "carlos@email.com", "12345678900")
            ),
            new MotoReadDto(
            2,
            "DEF5678",
            "Mottu Sport",
            "Em Trânsito",
            "CHS22345678901234",
            new ClienteResumoDto(2, "Maria Souza", "11987654321", 'F', "maria@email.com", "23456789011")
            ),
            new MotoReadDto(
            3,
            "GHI9101",
            "Mottu-E",
            "Inativa",
            "CHS32345678901234",
            new ClienteResumoDto(3, "João Mendes", "1188887777", 'M', "joao@email.com", "34567890122")
            )
        };

        var links = new List<LinkDto>
        {
            new LinkDto("self", "/motos?pageNumber=1&pageSize=10", "GET"),
            new LinkDto("create", "/motos", "POST"),
            new LinkDto("next", "/motos?pageNumber=2&pageSize=10", "GET"),
            new LinkDto("prev", "", "GET")
        };

        return new PagedResponse<MotoReadDto>(
            TotalCount: motos.Count,
            PageNumber: 1,
            PageSize: 10,
            TotalPages: 1,
            Data: motos,
            Links: links
        );
    }
}