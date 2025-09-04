using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class VagaService
{
    private readonly AppDbContext _db;

    public VagaService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetAllVagasAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Vagas.CountAsync();
        
        var vagas = await _db.Vagas
            .Include(v => v.Setor)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var vagasDto = vagas
            .Select(VagaReadDto.ToDto)
            .ToList();

        var response = new PagedResponse<VagaReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: vagasDto,
            Links: new List<LinkDto>
            {
                new("self", $"/vagas?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/vagas?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/vagas?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            });
        
        return vagasDto.Count != 0 ? Results.Ok(response) : Results.NoContent();
    }

    public async Task<IResult> GetVagaByIdAsync(int id)
    {
        var vaga = await _db.Vagas
            .Include(v => v.Setor)
            .FirstOrDefaultAsync(v => v.VagaId == id);

        if (vaga is null) return Results.NotFound("Nenhuma vaga encontrada com ID informado."); 
        
        var vagaDto = VagaReadDto.ToDto(vaga);

        var response = new ResourceResponse<VagaReadDto>(
            Data: vagaDto,
            Links: new List<LinkDto>
            {
                new("self", $"/vagas/{id}", "GET"),
                new("list", "/vagas", "GET")
            }
            );
        
        return Results.Ok(response);
    }
}