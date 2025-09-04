using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint3_API.Models;

public class Patio
{
    [Column("ID_PATIO")]
    public int PatioId { get; set; }
    
    [Column("LOCALIZACAO_PATIO")]
    [StringLength(100)]
    public required string LocalizacaoPatio { get; set; }
    
    [Column("NOME_PATIO")]
    [StringLength(100)]
    public required string NomePatio { get; set; }
    
    [Column("DESCRICAO_PATIO")]
    [StringLength(255)]
    public required string DescricaoPatio { get; set; }
    
    public required List<Setor> Setores { get; set; }
}