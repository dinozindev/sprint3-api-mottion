using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sprint3_API.Models;

public class Vaga
{
    [Column("ID_VAGA")]
    public int VagaId { get; set; }
    
    [Column("NUMERO_VAGA")]
    [StringLength(10)]
    public required string NumeroVaga { get; set; }
    
    [Column("STATUS_OCUPADA")]
    public required int StatusOcupada { get; set; }
    
    
    [Column("SETOR_ID_SETOR")]
    public required int SetorId { get; set; }
    
    [JsonIgnore]
    public required Setor Setor { get; set; }
}