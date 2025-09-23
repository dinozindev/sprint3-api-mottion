using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sprint3_API.Models;

public class Setor
{
    [Column("ID_SETOR")]
    public int SetorId { get; set; }
    
    [Column("TIPO_SETOR")]
    [StringLength(70)]
    public required string TipoSetor { get; set; }
    
    [Column("STATUS_SETOR")]
    [StringLength(50)]
    public required string StatusSetor { get; set; }
    
    [Column("PATIO_ID_PATIO")]
    public int PatioId { get; set; }
    
    [JsonIgnore]
    public required Patio Patio { get; set; }
    
    public required List<Vaga> Vagas { get; set; }
}