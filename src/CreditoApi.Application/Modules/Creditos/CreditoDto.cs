using CreditoApi.Domain.Creditos;

namespace CreditoApi.Application.Modules.Creditos;

public record CreditoDto
{
    public long Id { get; set; }
    public string NumeroCredito { get; init; } = string.Empty;
    public string NumeroNfse { get; init; } = string.Empty;
    public DateOnly DataConstituicao { get; init; }
    public decimal ValorIssqn { get; init; }
    public string TipoCredito { get; init; } = string.Empty;
    public string SimplesNacional { get; init; } = "Nao";
    public decimal Aliquota { get; init; }
    public decimal ValorFaturado { get; init; }
    public decimal ValorDeducao { get; init; }
    public decimal BaseCalculo { get; init; }

    public Credito MapToEntity()
    {
        return new Credito
        {
            Id = Id,
            NumeroCredito = NumeroCredito,
            NumeroNfse = NumeroNfse,
            DataConstituicao = DataConstituicao,
            ValorIssqn = ValorIssqn,
            TipoCredito = TipoCredito,
            SimplesNacional =
                SimplesNacional.ToLower().Equals("sim", StringComparison.OrdinalIgnoreCase) || (SimplesNacional.ToLower().Replace("ã", "a").Equals("nao", StringComparison.OrdinalIgnoreCase)
                    ? false : throw new ArgumentException(nameof(SimplesNacional), "Simples Nacional deve ser 'Sim' ou 'Não'")),
            Aliquota = Aliquota,
            ValorFaturado = ValorFaturado,
            ValorDeducao = ValorDeducao,
            BaseCalculo = BaseCalculo
        };
    }

    public static explicit operator CreditoDto(Credito credito)
    {
        return new CreditoDto
        {
            NumeroCredito = credito.NumeroCredito,
            NumeroNfse = credito.NumeroNfse,
            DataConstituicao = credito.DataConstituicao,
            ValorIssqn = credito.ValorIssqn,
            TipoCredito = credito.TipoCredito,
            SimplesNacional = credito.SimplesNacional ? "Sim" : "Nao",
            Aliquota = credito.Aliquota,
            ValorFaturado = credito.ValorFaturado,
            ValorDeducao = credito.ValorDeducao,
            BaseCalculo = credito.BaseCalculo
        };
    }
}
