using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprint3_API.Models;

public class Cargo
{
    [Column("ID_CARGO")]
    public int CargoId { get; set; }
    
    [Column("NOME_CARGO")]
    [StringLength(50)]
    public required string NomeCargo { get; set; }
    
    [Column("DESCRICAO_CARGO")]
    [StringLength(255)]
    public required string DescricaoCargo { get; set; }
}