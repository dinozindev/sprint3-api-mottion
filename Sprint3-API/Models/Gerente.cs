using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sprint3_API.Models;

public class Gerente
{
    [Column("ID_GERENTE")]
    public int GerenteId { get; set; }
    
    [Column("NOME_GERENTE")]
    [StringLength(100)]
    public required string NomeGerente { get; set; }
    
    [Column("TELEFONE_GERENTE")]
    [StringLength(11)]
    public required string TelefoneGerente { get; set; }
    
    [Column("CPF_GERENTE")]
    [StringLength(11)]
    public required string CpfGerente { get; set; }
    
    [Column("PATIO_ID_PATIO")]
    public required int PatioId { get; set; }
    
    [JsonIgnore]
    public required Patio Patio { get; set; }
}