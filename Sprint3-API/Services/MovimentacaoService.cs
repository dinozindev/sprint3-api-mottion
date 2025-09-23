using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Sprint3_API.Models;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class MovimentacaoService
{
    private readonly AppDbContext _db;
    private readonly IHubContext<SetorHub> _hubContext;
    
    private static readonly HashSet<string> SetoresInativa = new()
    {
        "Pendência", "Sem Placa", "Agendada Para Manutenção"
    };

    private static readonly HashSet<string> SetoresManutencao = new()
    {
        "Reparos Simples", "Danos Estruturais Graves", "Motor Defeituoso"
    };

    private static readonly HashSet<string> SetoresAtiva = new()
    {
        "Minha Mottu", "Pronta para Aluguel"
    };

    public MovimentacaoService(AppDbContext db, IHubContext<SetorHub> hubContext)
    {
        _db = db;
        _hubContext = hubContext;
    }

    public async Task<IResult> GetAllMovimentacoesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Movimentacoes.CountAsync();
        
        var movimentacoes = await _db.Movimentacoes
            .Include(m => m.Moto)
            .ThenInclude(mo => mo.Cliente)
            .Include(m => m.Vaga)
            .ThenInclude(v => v.Setor)
            .ThenInclude(s => s.Patio)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var movimentacoesDto = movimentacoes.Select(MovimentacaoReadDto.ToDto).ToList();

        var response = new PagedResponse<MovimentacaoReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: movimentacoesDto,
            Links: new List<LinkDto>
            {
                new("self", $"/movimentacoes?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("create", $"/movimentacoes", "POST"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/movimentacoes?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/movimentacoes?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );
        
        return movimentacoesDto.Count != 0 ? Results.Ok(response) : Results.NoContent();
    }

    public async Task<IResult> GetMovimentacaoByIdAsync(int id)
    {
        var movimentacao = await _db.Movimentacoes
            .Include(m => m.Moto)
            .ThenInclude(m => m.Cliente)
            .Include(m => m.Vaga)
            .ThenInclude(v => v.Setor)
            .ThenInclude(s => s.Patio)
            .FirstOrDefaultAsync(s => s.MovimentacaoId == id);

        if (movimentacao is null) return Results.NotFound("Nenhuma Movimentação encontrada com ID informado.");
        
        var movimentacaoDto = MovimentacaoReadDto.ToDto(movimentacao);

        var response = new ResourceResponse<MovimentacaoReadDto>(
            Data: movimentacaoDto,
            Links: new List<LinkDto>
            {
                new("self", $"movimentacoes/{id}", "GET"),
                new("update", $"movimentacoes/{id}", "PUT"),
                new("delete", $"movimentacoes/{id}", "DELETE"),
                new("list", "/movimentacoes", "GET") 
            }
            );
        
        return Results.Ok(response);
    }

    public async Task<IResult> GetMovimentacoesByMotoIdAsync(int motoId, int pageNumber = 1, int pageSize = 10)
    {
        var moto = await _db.Movimentacoes
            .Where(m => m.MotoId == motoId)
            .FirstOrDefaultAsync();
        
        if (moto is null) return Results.NotFound("Nenhuma movimentação encontrada para a Moto informada.");
        
        var totalCount = await _db.Movimentacoes.CountAsync();
        
        var movimentacoes = await _db.Movimentacoes
            .Where(m => m.MotoId == motoId)
                .Include(m => m.Moto)
                .ThenInclude(mo => mo.Cliente)
                .Include(m => m.Vaga)
                .ThenInclude(v => v.Setor)
                .ThenInclude(s => s.Patio)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        var movimentacoesDto = movimentacoes.Select(MovimentacaoReadDto.ToDto).ToList();

        var response = new PagedResponse<MovimentacaoReadDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: movimentacoesDto,
            Links: new List<LinkDto>
            {
                new("self", $"/movimentacoes/por-moto/{motoId}?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("create", $"/movimentacoes", "POST"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/movimentacoes/por-moto/{motoId}?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/movimentacoes/por-moto/{motoId}?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );

        return Results.Ok(response);
    }

    public async Task<IResult> GetTotalVagasOcupadasPatioAsync(int id, int pageNumber = 1, int pageSize = 10)
    {
        var patio = await _db.Patios.Where(s => s.PatioId == id).FirstOrDefaultAsync();
        
        if (patio is null) return Results.NotFound("Nenhum Pátio encontrado com ID informado.");
        
        var totalCount = await _db.Setores.Where(s => s.PatioId == id).CountAsync();
        
        var totalVagasSetor = await _db.Setores
            .Where(s => s.PatioId == id)
            .Select(s => new VagasSetorDto
            (
                s.TipoSetor,
                _db.Vagas.Count(v => v.SetorId == s.SetorId),
                _db.Movimentacoes.Count(m =>
                    m.DtSaida == null &&
                    _db.Vagas
                        .Where(v => v.SetorId == s.SetorId)
                        .Select(v => v.VagaId)
                        .Contains(m.VagaId)
                )))
            .ToListAsync();

        var response = new PagedResponse<VagasSetorDto>(
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: totalVagasSetor,
            Links: new List<LinkDto>
            {
                new("self", $"/movimentacoes/ocupacao-por-setor/patio/{id}?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("create", $"/movimentacoes", "POST"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/movimentacoes/ocupacao-por-setor/patio/{id}?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/movimentacoes/ocupacao-por-setor/patio/{id}?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
            );

        return totalVagasSetor.Count != 0 ? Results.Ok(response) : Results.NoContent();
    }

    public async Task<IResult> CreateMovimentacaoAsync(MovimentacaoPostDto dto)
    {
        
        // Procura a moto e a vaga para verificar se existem ou não
        var moto = await _db.Motos
            .Include(m => m.Cliente) 
            .FirstOrDefaultAsync(m => m.MotoId == dto.MotoId);
    
        var vaga = await _db.Vagas
            .Include(v => v.Setor) 
            .FirstOrDefaultAsync(v => v.VagaId == dto.VagaId);
    
        if (moto == null || vaga == null)
        {
            return Results.NotFound("Moto ou vaga não encontrada.");
        }
        
        var movimentacao = new Movimentacao
        {
            DescricaoMovimentacao = dto.DescricaoMovimentacao,
            Moto = moto,
            Vaga = vaga,
        };

    // Verifica se a moto já está em uma movimentação ativa
    var movAtivaMoto = await _db.Movimentacoes
        .FirstOrDefaultAsync(m => m.MotoId == movimentacao.MotoId && m.DtSaida == null);
    if (movAtivaMoto != null)
    {
        return Results.Conflict("Esta moto já está em uma movimentação ativa.");
    }

    // Verifica se a vaga já está ocupada
    var movAtivaVaga = await _db.Movimentacoes
        .FirstOrDefaultAsync(m => m.VagaId == movimentacao.VagaId && m.DtSaida == null);
    if (movAtivaVaga != null)
    {
        return Results.Conflict("Esta vaga já está ocupada.");
    }
    
    // Define a data de entrada e saída (nula)
    movimentacao.DtEntrada = DateTime.Now;
    movimentacao.DtSaida = null;
    
    // Define a situação da moto baseada no setor em que foi estacionada
    string tipoSetor = vaga.Setor.TipoSetor;
    if (SetoresInativa.Contains(tipoSetor))
        moto.SituacaoMoto = "Inativa";
    else if (SetoresManutencao.Contains(tipoSetor))
        moto.SituacaoMoto = "Manutenção";
    else if (SetoresAtiva.Contains(tipoSetor))
        moto.SituacaoMoto = "Ativa";

    // Atualiza status da vaga
    vaga.StatusOcupada = 1;

    _db.Movimentacoes.Add(movimentacao);
    await _db.SaveChangesAsync();
    
    var movimentacaoDto = MovimentacaoReadDto.ToDto(movimentacao);
    
    int patioId = vaga.Setor.PatioId;
    
    // retorna ao Front os setores atualizados
    var setoresAtualizados = await _db.Setores
        .Where(s => s.PatioId == patioId)
        .Select(s => new
        {
            Setor = s.TipoSetor,
            TotalVagas = _db.Vagas.Count(v => v.SetorId == s.SetorId),
            MotosPresentes = _db.Movimentacoes.Count(m =>
                m.DtSaida == null &&
                _db.Vagas.Where(v => v.SetorId == s.SetorId).Select(v => v.VagaId).Contains(m.VagaId))
        })
        .ToListAsync();
    
    await _hubContext.Clients.Group($"patio-{patioId}")
        .SendAsync("AtualizarOcupacaoTodosSetores", new
        {
            PatioId = patioId,
            Setores = setoresAtualizados
        });

    var response = new ResourceResponse<MovimentacaoReadDto>(
        Data: movimentacaoDto,
        Links: new List<LinkDto>
        {
            new("self", $"/movimentacoes/{movimentacaoDto.MovimentacaoId}", "GET"),
            new("update", $"/movimentacoes/{movimentacaoDto.MovimentacaoId}", "PUT"),
            new("delete", $"movimentacoes/{movimentacaoDto.MovimentacaoId}", "DELETE"),
            new("list", "/movimentacoes", "GET")
        });
    
    return Results.Created($"/movimentacoes/{movimentacao.MovimentacaoId}", response);
    }

    public async Task<IResult> UpdateMovimentacaoAsync(int id)
    {
        var movimentacao = await _db.Movimentacoes
            .Include(m => m.Moto)
            .Include(m => m.Vaga)
            .ThenInclude(v => v.Setor)
            .FirstOrDefaultAsync(m => m.MovimentacaoId == id);

        // Verifica se a movimentação existe.
        if (movimentacao is null)
        {
            return Results.NotFound("Movimentação não encontrada.");
        }
    
        // Verifica se a movimentação já foi finalizada
        if (movimentacao.DtSaida != null)
        {
            return Results.BadRequest("Esta movimentação já foi finalizada.");
        }
        
        // Atualiza a data de saída
        movimentacao.DtSaida = DateTime.Now;

        // Atualiza status da vaga para desocupada
        movimentacao.Vaga.StatusOcupada = 0;

        // Atualiza a situação da moto para 'Em Trânsito'
        movimentacao.Moto.SituacaoMoto = "Em Trânsito";
        
        var movimentacaoDto = MovimentacaoReadDto.ToDto(movimentacao);
    
        await _db.SaveChangesAsync();
    
        int patioId = movimentacao.Vaga.Setor.PatioId;

        var setoresAtualizados = await _db.Setores
            .Where(s => s.PatioId == patioId)
            .Select(s => new
            {
                Setor = s.TipoSetor,
                TotalVagas = _db.Vagas.Count(v => v.SetorId == s.SetorId),
                MotosPresentes = _db.Movimentacoes.Count(m =>
                    m.DtSaida == null &&
                    _db.Vagas.Where(v => v.SetorId == s.SetorId).Select(v => v.VagaId).Contains(m.VagaId))
            })
            .ToListAsync();
    
        await _hubContext.Clients.Group($"patio-{patioId}")
            .SendAsync("AtualizarOcupacaoTodosSetores", new
            {
                PatioId = patioId,
                Setores = setoresAtualizados
            });
        
        var response = new ResourceResponse<MovimentacaoReadDto>(
            Data: movimentacaoDto,
            Links: new List<LinkDto>
            {
                new("self", $"/movimentacoes/{movimentacaoDto.MovimentacaoId}", "GET"),
                new("update", $"movimentacoes/{movimentacaoDto.MovimentacaoId}", "PUT"),
                new("delete", $"movimentacoes/{movimentacaoDto.MovimentacaoId}", "DELETE"),
                new("list", "/movimentacoes", "GET")
            });

        return Results.Ok(response);
    }
    
    // deleta uma movimentação pelo ID
    public async Task<IResult> DeleteMovimentacaoAsync(int id)
    {
        var movimentacao = await _db.Movimentacoes.FindAsync(id);
        if (movimentacao is null) return Results.NotFound("Movimentação não encontrada com ID especificado.");

        _db.Movimentacoes.Remove(movimentacao);
        await _db.SaveChangesAsync();
        return Results.NoContent();
    }
}