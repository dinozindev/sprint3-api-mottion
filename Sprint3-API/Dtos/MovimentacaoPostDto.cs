namespace Sprint3_API.Dtos;

public record MovimentacaoPostDto(
    string DescricaoMovimentacao,
    int MotoId,
    int VagaId);