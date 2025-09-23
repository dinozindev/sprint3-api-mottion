using Swashbuckle.AspNetCore.Filters;
using Sprint3_API.Dtos;

namespace Sprint3_API.Examples;

public class ClientePostDtoExample : IExamplesProvider<ClientePostDto>
{
    public ClientePostDto GetExamples()
    {
        return new ClientePostDto(
            "Marcos Dos Santos",
            "11948372632",
            'M',
            "mdsantos@gmail.com",
            "43221254321"
        );
    }
}