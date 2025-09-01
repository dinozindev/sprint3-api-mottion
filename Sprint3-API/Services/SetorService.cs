using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class SetorService
{
    private readonly AppDbContext _db;

    public SetorService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetAllSetoresAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Setores.CountAsync();
        
        var setores = await _db.Setores
            .Include(s => s.Patio)
            .Include(s => s.Vagas)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var setoresDto = setores
            .Select(SetorReadDto.ToDto)
            .ToList();

        var response = new PagedResponse<SetorReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: setoresDto,
            Links: new List<LinkDto>
            {
                new("self", $"/setores?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/setores?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/setores?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );
        
        return setoresDto.Any() ? Results.Ok(response) : Results.NoContent();
    }

    public async Task<IResult> GetSetorByIdAsync(int id)
    {
        var setor = await _db.Setores
            .Include(s => s.Patio)
            .Include(s => s.Vagas)
            .FirstOrDefaultAsync(s => s.SetorId == id);

        if (setor is null) return Results.NotFound("Nenhum setor encontrado com ID informado.");
        
        var setorDto = SetorReadDto.ToDto(setor);

        var response = new ResourceResponse<SetorReadDto>(
            Data: setorDto,
            Links: new List<LinkDto>
            {
                new("self", $"setores/{id}", "GET"),
                new("list", "/setores", "GET")
            }
            );
        
        return Results.Ok(response);
    }
}