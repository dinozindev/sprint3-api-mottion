using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class PatioService
{
    private readonly AppDbContext _db;

    public PatioService(AppDbContext db)
    {
        _db = db;
    }
    
    // retorna todos os patios
    public async Task<IResult> GetAllPatiosAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Patios.CountAsync();
        
        var patios = await _db.Patios
            .Include(p => p.Setores)
            .ThenInclude(s => s.Vagas)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var patiosDto = patios.Select(PatioReadDto.ToDto).ToList();

        var response = new PagedResponse<PatioReadDto>(
            TotalCount : totalCount,
            PageNumber : pageNumber,
            PageSize : pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: patiosDto,
            Links: new List<LinkDto>
            {
                new("self", $"/patios?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/patios?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/patios?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            });
        
        return patiosDto.Any() ? Results.Ok(response) : Results.NoContent();
    }
    
    // retorna patio por ID
    public async Task<IResult> GetPatioByIdAsync(int id)
    {
        var patio = await _db.Patios
            .Include(p => p.Setores)
            .ThenInclude(s => s.Vagas)
            .FirstOrDefaultAsync(p => p.PatioId == id);

        if (patio is null) return Results.NotFound("Nenhum pátio encontrado com ID informado.");
            
        var patioDto = PatioReadDto.ToDto(patio);

        var response = new ResourceResponse<PatioReadDto>(
            Data: patioDto,
            Links: new List<LinkDto>
            {
                new("self", $"/patios/{id}", "GET"),
                new("list", "/patios", "GET")
            }
            );
            
        return Results.Ok(response);
    }
}