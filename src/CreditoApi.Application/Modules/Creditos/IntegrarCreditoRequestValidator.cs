using CreditoApi.Application.Extensions;
using FluentValidation;

namespace CreditoApi.Application.Modules.Creditos;

public sealed class IntegrarCreditoRequestValidator : AbstractValidator<IntegrarCreditoRequest>
{
    public IntegrarCreditoRequestValidator()
    {
        RuleFor(x => x.Creditos)
            .NotNull()
            .NotEmpty()
            .WithError(CreditoError.ListaDeCreditosInvalida);

        RuleForEach(x => x.Creditos)
            .SetValidator(new CreditoDtoValidator());

        RuleFor(x => x.Creditos)
            .Must(list => list.Select(c => c.NumeroCredito).Distinct().Count() == list.Count)
            .WithError(CreditoError.NumeroCreditoDeveSerUnico);
    }
}
