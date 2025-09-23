using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class GerenteService
{
    private readonly AppDbContext _db;

    public GerenteService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetAllGerentesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Cargos.CountAsync();
        
        var gerentes = await _db.Gerentes
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(g => g.Patio)
            .ToListAsync();

        var gerentesDto = gerentes.Select(GerenteReadDto.ToDto).ToList();

        var response = new PagedResponse<GerenteReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: gerentesDto,
            Links: new List<LinkDto>
            {
                new("self", $"/gerentes?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/gerentes?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/gerentes?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );
        
        return gerentesDto.Count != 0 ? Results.Ok(response) : Results.NoContent();
    }

    public async Task<IResult> GetGerenteByIdAsync(int id)
    {
        var gerente = await _db.Gerentes
            .Include(g => g.Patio)
            .FirstOrDefaultAsync(g => g.GerenteId == id);

        if (gerente is null) return Results.NotFound("Nenhum Gerente encontrado com ID informado.");
        
        var gerenteDto = GerenteReadDto.ToDto(gerente);

        var response = new ResourceResponse<GerenteReadDto>(
            Data: gerenteDto,
            Links: new List<LinkDto>
            {
                new("self", $"/gerentes/{id}", "GET"),
                new("list", "/gerentes", "GET")
            });
        
        return Results.Ok(response);
    }
}