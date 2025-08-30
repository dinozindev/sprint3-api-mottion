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
    public async Task<IResult> GetAllCargosAsync()
    {
        var cargos = await _db.Cargos.ToListAsync();
        var cargosDto = cargos.Select(CargoReadDto.ToDto).ToList();
        return cargosDto.Any() ? Results.Ok(cargosDto) : Results.NoContent();
    }
    
    // retorna um cargo pelo ID
    public async Task<IResult> GetCargoByIdAsync(int id)
    {
        var cargo = await _db.Cargos.FindAsync(id);
        return cargo is null 
            ? Results.NotFound("Nenhum cargo encontrado com ID informado.") 
            : Results.Ok(CargoReadDto.ToDto(cargo));
    }
}