using Microsoft.EntityFrameworkCore;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class FuncionarioService
{
    private readonly AppDbContext _db;

    public FuncionarioService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<IResult> GetAllFuncionariosAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Funcionarios.CountAsync();
        
        var funcionarios = await _db.Funcionarios
            .Include(f => f.Cargo)
            .Include(f => f.Patio)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var funcionariosDto = funcionarios.Select(FuncionarioReadDto.ToDto).ToList();

        var response = new PagedResponse<FuncionarioReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: funcionariosDto,
            Links: new List<LinkDto>
            {
                new("self", $"/funcionarios?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/funcionarios?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/funcionarios?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );
        
        return funcionariosDto.Any() ? Results.Ok(response) : Results.NoContent();
        
    }

    public async Task<IResult> GetFuncionarioByIdAsync(int id)
    {
        var funcionario = await _db.Funcionarios
            .Include(f => f.Cargo)
            .Include(f => f.Patio)
            .FirstOrDefaultAsync(f => f.FuncionarioId == id);
        
        if (funcionario is null) return Results.NotFound("Nenhum funcionário encontrado com ID informado.");
          
        var funcionarioDto = FuncionarioReadDto.ToDto(funcionario);

        var response = new ResourceResponse<FuncionarioReadDto>(
            Data: funcionarioDto,
            Links: new List<LinkDto>
            {
                new("self", $"funcionarios/{id}", "GET"),
                new("list", "/funcionarios", "GET")
            }
            );
        
        return Results.Ok(response);
    }
} 