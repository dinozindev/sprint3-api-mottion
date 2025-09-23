using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Sprint3_API.Models;
using Sprint3_API.Dtos;

namespace Sprint3_API.Services;

public class MotoService
{
    private readonly AppDbContext _db;
    private const string motosString = "/motos";
    private const string updateString = "update";
    private const string deleteString = "delete";
    private const string deleteMethod = "DELETE";
    
    public MotoService(AppDbContext db)
    {
        _db = db;
    }

    // retorna todas as motos
    public async Task<IResult> GetAllMotosAsync(int pageNumber = 1, int pageSize = 10)
    {
        var totalCount = await _db.Motos.CountAsync();
        
        var motos = await _db.Motos
            .Include(m => m.Cliente)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        var motosDto = motos.Select(MotoReadDto.ToDto).ToList();
        
        var response = new PagedResponse<MotoReadDto>
        (
            TotalCount: totalCount,
            PageNumber: pageNumber,
            PageSize: pageSize,
            TotalPages: (int)Math.Ceiling(totalCount / (double)pageSize),
            Data: motosDto,
            Links: new List<LinkDto>
            {
                new("self", $"/motos?pageNumber={pageNumber}&pageSize={pageSize}", "GET"),
                new("create", motosString, "POST"),
                new("next", pageNumber < (int)Math.Ceiling(totalCount / (double)pageSize) ? $"/motos?pageNumber={pageNumber+1}&pageSize={pageSize}" : string.Empty, "GET"),
                new("prev", pageNumber > 1 ? $"/motos?pageNumber={pageNumber-1}&pageSize={pageSize}" : string.Empty, "GET")
            }
        );
        
        return motosDto.Count != 0 ? Results.Ok(response) : Results.NoContent();
    } 
    
    // retorna a moto pelo ID
    public async Task<IResult> GetMotoByIdAsync(int id)
    {
        var moto = await _db.Motos
            .Include(m => m.Cliente)
            .FirstOrDefaultAsync(m => m.MotoId == id);

        if (moto is null) return Results.NotFound("Nenhuma Moto encontrada com o ID informado.");
        
        var motoDto = MotoReadDto.ToDto(moto);

        var response = new ResourceResponse<MotoReadDto>(
            Data: motoDto,
            Links: new List<LinkDto>
            {
                new("self", $"/motos/{id}", "GET"),
                new(updateString, $"/motos/{id}", "PUT"),
                new(deleteString, $"/motos/{id}", deleteMethod),
                new("list", motosString, "GET")
            }
            );
        
        return Results.Ok(response);
    }
    
    // retorna uma moto pelo número de Chassi
    public async Task<IResult> GetMotoByChassiAsync(string numeroChassi)
    {
        var moto = await _db.Motos
            .Include(m => m.Cliente)
            .FirstOrDefaultAsync(m => m.ChassiMoto == numeroChassi);

        if (moto is null) return Results.NotFound("Nenhuma Moto encontrada com o número de chassi informado.");
        
        var motoDto = MotoReadDto.ToDto(moto);

        var response = new ResourceResponse<MotoReadDto>(
            Data: motoDto,
            Links: new List<LinkDto>
            {
                new(updateString, $"/motos/{motoDto.MotoId}", "PUT"),
                new(deleteString, $"/motos/{motoDto.MotoId}", deleteMethod),
                new("list", motosString, "GET")
            }
            );
            
        return Results.Ok(response);
    }
    
    // busca a ultima posição que a moto esteve
    public async Task<IResult> GetMotoUltimaPosicaoAsync(int id)
    {
        var ultimaMovimentacao = await _db.Movimentacoes
            .Where(m => m.MotoId == id) 
            .OrderByDescending(m => m.DtEntrada)
            .Include(m => m.Vaga)
            .ThenInclude(v => v.Setor)
            .FirstOrDefaultAsync();

        if (ultimaMovimentacao is null) return Results.NotFound("Movimentação não encontrada para essa moto.");

        var ultimaPosicao = new UltimaPosicaoDto(
            new VagaResumoDto(
                ultimaMovimentacao.Vaga.VagaId,
                ultimaMovimentacao.Vaga.NumeroVaga,
                ultimaMovimentacao.Vaga.StatusOcupada
            ),
            new SetorResumoDto(
                ultimaMovimentacao.Vaga.Setor.SetorId,
                ultimaMovimentacao.Vaga.Setor.TipoSetor,
                ultimaMovimentacao.Vaga.Setor.StatusSetor,
                ultimaMovimentacao.Vaga.Setor.PatioId
            ),
            ultimaMovimentacao.DtEntrada,
            ultimaMovimentacao.DtSaida,
            ultimaMovimentacao.DtSaida is null
        );

        var response = new ResourceResponse<UltimaPosicaoDto>(
            Data: ultimaPosicao,
            Links: new List<LinkDto>
            {
                new("self", $"/motos/{id}", "GET"),
                new(updateString, $"/motos/{id}", "PUT"),
                new(deleteString, $"/motos/{id}", deleteMethod),
                new("list", motosString, "GET")
            });

        return Results.Ok(response);
    }

    // cria uma moto
    public async Task<IResult> CreateMotoAsync(MotoPostDto dto)
    {
        var validation = await ValidateMoto(dto);
        if (validation is not null) return validation;
        
        var moto = new Moto()
        {
            PlacaMoto = dto.PlacaMoto,
            ModeloMoto = dto.ModeloMoto,
            SituacaoMoto = dto.SituacaoMoto,
            ChassiMoto = dto.ChassiMoto,
            ClienteId = null
        };
        
        _db.Motos.Add(moto);
        await _db.SaveChangesAsync();
        
        var motoDto = MotoReadDto.ToDto(moto);

        var response = new ResourceResponse<MotoReadDto>(
            Data: motoDto,
            Links: new List<LinkDto>
            {
                new("self", $"/motos/{motoDto.MotoId}", "GET"),
                new(updateString, $"/motos/{motoDto.MotoId}", "PUT"),
                new(deleteString, $"/motos/{motoDto.MotoId}", deleteMethod),
                new("list", motosString, "GET")
            }
            );
        
        return Results.Created($"/motos/{moto.MotoId}", response);
    }

    // atualiza os dados de uma moto
    public async Task<IResult> UpdateMotoAsync(int id, MotoPostDto dto)
    {
        var motoExistente = await _db.Motos.FindAsync(id);
        if (motoExistente is null) return Results.NotFound("Moto não encontrada com ID informado.");
        
        var validation = await ValidateMoto(dto);
        if (validation is not null) return validation;
        
        motoExistente.PlacaMoto = dto.PlacaMoto;
        motoExistente.ModeloMoto = dto.ModeloMoto;
        motoExistente.SituacaoMoto = dto.SituacaoMoto;
        motoExistente.ChassiMoto = dto.ChassiMoto;

        await _db.SaveChangesAsync();
        
        var motoDto = MotoReadDto.ToDto(motoExistente);

        var response = new ResourceResponse<MotoReadDto>(
            Data: motoDto,
            Links: new List<LinkDto>
            {
                new("self", $"/motos/{motoDto.MotoId}", "GET"),
                new(deleteString, $"/motos/{motoDto.MotoId}", deleteMethod),
                new("list", motosString, "GET")
            });
        
        return Results.Ok(response);
    }
    
    // deleta uma moto pelo ID
    public async Task<IResult> DeleteMotoAsync(int id)
    {
        var moto = await _db.Motos.FindAsync(id);
        if (moto is null) return Results.NotFound("Moto não encontrada com ID informado.");
        
        _db.Motos.Remove(moto);
        await _db.SaveChangesAsync();
        return Results.NoContent();
    }

    // remove associação de Cliente de uma Moto e depois atualiza
    public async Task<IResult> DeleteAssociacaoClienteMotoAsync(int id)
    {
        var moto = await _db.Motos.FindAsync(id);
        if (moto is null) return Results.NotFound("Nenhuma moto encontrada com o ID informado.");

        moto.ClienteId = null;

        await _db.SaveChangesAsync();
        
        return Results.NoContent();
    }
    
    // atualiza a associação de uma moto com um cliente
    public async Task<IResult> UpdateAssociacaoClienteMotoAsync(int idMoto, int idCliente)
    {
        var moto = await _db.Motos.FindAsync(idMoto);
        if (moto is null) return Results.NotFound("Nenhuma moto encontrada com o ID informado.");
        
        var cliente = await _db.Clientes.FindAsync(idCliente);
        if (cliente is null) return Results.NotFound("Nenhum cliente encontrado com o ID informado.");
        
        moto.ClienteId = idCliente;
        await _db.SaveChangesAsync();

        return Results.NoContent();
    }

        private async Task<IResult?> ValidateMoto(MotoPostDto dto, int? ignoreId = null)
    {
        // verifica se a placa está no formato correto
        if (!string.IsNullOrWhiteSpace(dto.PlacaMoto))
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(dto.PlacaMoto, @"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$", RegexOptions.None, TimeSpan.FromMilliseconds(100)))
            {
                return Results.BadRequest("Placa inválida. Use o formato ABC1234 ou ABC1D23.");
            }

            // verifica se a placa já existe usando CountAsync
            var placaExistente = await _db.Motos
                .Where(m => m.PlacaMoto == dto.PlacaMoto 
                            && (!ignoreId.HasValue || m.MotoId != ignoreId.Value))
                .CountAsync() > 0;
            
            if (placaExistente)
                return Results.Conflict($"Já existe uma moto com a placa '{dto.PlacaMoto}'.");
        }

        // verifica se o Chassi está no formato correto
        if (string.IsNullOrWhiteSpace(dto.ChassiMoto) ||
            !System.Text.RegularExpressions.Regex.IsMatch(dto.ChassiMoto, @"^[A-HJ-NPR-Z0-9]{17}$", RegexOptions.None, TimeSpan.FromMilliseconds(100)))
        {
            return Results.BadRequest("Chassi inválido. Deve conter 17 caracteres alfanuméricos, sem I, O ou Q.");
        }

        // verifica se o chassi já existe usando CountAsync
        var chassiExistente = await _db.Motos
            .Where(m => m.ChassiMoto == dto.ChassiMoto 
                        && (!ignoreId.HasValue || m.MotoId != ignoreId.Value))
            .CountAsync() > 0;
        
        if (chassiExistente)
            return Results.Conflict($"Já existe uma moto com o chassi '{dto.ChassiMoto}'.");

        // valida modelos e situações
        var modelosValidos = new[] { "Mottu Pop", "Mottu Sport", "Mottu-E" };
        if (!modelosValidos.Contains(dto.ModeloMoto))
            return Results.BadRequest("Modelo inválido. Os modelos válidos são: Mottu Pop, Mottu Sport, Mottu-E.");
        
        var situacoesValidas = new[] { "Ativa", "Inativa", "Manutenção", "Em Trânsito" };
        if (!situacoesValidas.Contains(dto.SituacaoMoto))
            return Results.BadRequest("Situação inválida. As situações válidas são: Ativa, Inativa, Manutenção, Em Trânsito.");

        return null;
    }

}