using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sprint3_API.Models;

public class Funcionario
{
    [Column("ID_FUNCIONARIO")]
    public int FuncionarioId { get; set; }
    
    [Column("NOME_FUNCIONARIO")]
    [StringLength(100)]
    public required string NomeFuncionario { get; set; }
    
    [Column("TELEFONE_FUNCIONARIO")]
    [StringLength(11)]
    public required string TelefoneFuncionario { get; set; }
    
    [Column("CARGO_ID_CARGO")]
    public required int CargoId { get; set; }
    
    [Column("PATIO_ID_PATIO")]
    public required int PatioId { get; set; }

    [JsonIgnore]
    public required Cargo Cargo { get; set; }
    
    [JsonIgnore]
    public required Patio Patio { get; set; }
}