using CreditoApi.Application.Extensions;
using FluentValidation;

namespace CreditoApi.Application.Modules.Creditos;

internal sealed class CreditoDtoValidator : AbstractValidator<CreditoDto>
{
    public CreditoDtoValidator()
    {
        RuleFor(x => x.NumeroCredito)
            .NotEmpty()
            .MaximumLength(50)
            .WithError(CreditoError.NumeroCreditoObrigatorio);

        RuleFor(x => x.NumeroNfse)
            .NotEmpty()
            .MaximumLength(50)
            .WithError(CreditoError.NumeroNfseObrigatorio);

        RuleFor(x => x.DataConstituicao)
            .NotNull()
            .NotEqual(DateOnly.MinValue).NotEqual(DateOnly.MaxValue)
            .WithError(CreditoError.DataConstituicaoInvalida);

        RuleFor(x => x.ValorIssqn)
            .NotNull()
            .GreaterThanOrEqualTo(0.0m)
            .WithError(CreditoError.ValorIssqnInvalido);

        RuleFor(x => x.SimplesNacional)
            .NotNull()
            .NotEmpty()
            .MaximumLength(3)
            .Must(s =>
            {
                string str = FlagSimplesNacionalTratada(s);
                return str.Equals("sim") || str.Equals("nao");
            })
            .WithError(CreditoError.FlagSimplesNacionalInvalida);
    }

    private static string FlagSimplesNacionalTratada(string simplesNacional)
    {
        return simplesNacional
            .ToLower()
            .Replace("ã", "a");
    }
}
