using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sprint3_API.Models;

[Table("CLIENTE")] 
public class Cliente
{
    [Column("ID_CLIENTE")]
    public int ClienteId { get; set; }

    [Column("TELEFONE_CLIENTE")]
    [StringLength(11)]
    public required string TelefoneCliente { get; set; }

    [Column("NOME_CLIENTE")]
    [StringLength(100)]
    public required string NomeCliente { get; set; }

    [Column("SEXO_CLIENTE")]
    [StringLength(1)]
    public required char SexoCliente { get; set; }

    [Column("EMAIL_CLIENTE")]
    [StringLength(100)]
    public required string EmailCliente { get; set; }
    
    [StringLength(11)]
    [Column("CPF_CLIENTE")]
    public required string CpfCliente { get; set; }

    public required List<Moto> Motos { get; set; }
}