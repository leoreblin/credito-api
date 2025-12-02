using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CreditoApi.SharedKernel.Primitives;
using Microsoft.EntityFrameworkCore;

namespace CreditoApi.Domain.Creditos;

[Table("credito", Schema = "credito_api")]
[Index(nameof(NumeroCredito), IsUnique = true)]
public sealed class Credito : BaseEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new long Id { get; init; }

    [Required]
    [MaxLength(50)]
    [Column("numero_credito")]
    public string NumeroCredito { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    [Column("numero_nfse")]
    public string NumeroNfse { get; set; } = string.Empty;

    [Column("data_constituicao", TypeName = "date")]
    public DateOnly DataConstituicao { get; set; }

    [Column("valor_issqn")]
    [Precision(15, 2)]
    public decimal ValorIssqn { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("tipo_credito")]
    public string TipoCredito { get; set; } = string.Empty;

    [Column("simples_nacional")]
    public bool SimplesNacional { get; set; }

    [Column("aliquota")]
    [Precision(5, 2)]
    public decimal Aliquota { get; set; }

    [Column("valor_faturado")]
    [Precision(15, 2)]
    public decimal ValorFaturado { get; set; }

    [Column("valor_deducao")]
    [Precision(15, 2)]
    public decimal ValorDeducao { get; set; }

    [Column("base_calculo")]
    [Precision(15, 2)]
    public decimal BaseCalculo { get; set; }
}
