using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class CargoService
{
    private readonly AppDbContext _db;

    public CargoService(AppDbContext db)
    {
        _db = db;
    }

    // retorna todos os cargos
    public async Task<IResult> GetAllCargosAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Cargos.CountAsync();
        
        var cargos = await _db.Cargos
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var cargosDto = cargos.Select(CargoReadDto.ToDto).ToList();

        var response = new PagedResponse<CargoReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: cargosDto,
            Links: new List<LinkDto>
            {
                new("self", $"/cargos?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/cargos?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/cargos?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );
        
        return cargosDto.Any() ? Results.Ok(response) : Results.NoContent();
    }
    
    // retorna um cargo pelo ID
    public async Task<IResult> GetCargoByIdAsync(int id)
    {
        var cargo = await _db.Cargos.FindAsync(id);
        if (cargo is null) return Results.NotFound("Nenhum cargo encontrado com ID informado.");
        
        var cargoDto = CargoReadDto.ToDto(cargo);

        var response = new ResourceResponse<CargoReadDto>(
            Data: cargoDto,
            Links: new List<LinkDto>
            {
                new("self", $"cargos/{id}", "GET"),
                new("list", "/cargos", "GET")
            }
            );
        
        return Results.Ok(response);
    }
}