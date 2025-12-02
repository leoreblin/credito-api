using CreditoApi.SharedKernel.Errors;

namespace CreditoApi.Application.Modules.Creditos;

public static class CreditoError
{
    public static Error ListaDeCreditosInvalida =>
        new(
            code: "Credito.ListaDeCreditosInvalida",
            description: "A lista de créditos não pode ser vazia.",
            type: ErrorType.Validation);

    public static Error NumeroCreditoObrigatorio =>
        new(
            code: "Credito.NumeroCreditoObrigatorio",
            description: "O número do crédito é obrigatório.",
            type: ErrorType.Validation);

    public static Error NumeroCreditoDeveSerUnico =>
        new(
            code: "Credito.NumeroCreditoDeveSerUnico",
            description: "A lista de créditos contém números de crédito duplicados.",
            type: ErrorType.Validation);

    public static Error NumeroNfseObrigatorio =>
        new(
            code: "Credito.NumeroNfseObrigatorio",
            description: "O número da NFS-e é obrigatório.",
            type: ErrorType.Validation);

    public static Error DataConstituicaoInvalida =>
        new(
            code: "Credito.DataConstituicaoInvalida",
            description: "A data de constituição informada é inválida.",
            type: ErrorType.Validation);

    public static Error ValorIssqnInvalido =>
        new(
            code: "Credito.ValorIssqnInvalido",
            description: "O valor do ISSQN deve ser maior ou igual a zero.",
            type: ErrorType.Validation);

    public static Error FlagSimplesNacionalInvalida =>
        new(
            code: "Credito.FlagSimplesNacionalInvalida",
            description: "O campo Simples Nacional informado deve ser 'Sim' ou 'Não'.",
            type: ErrorType.Validation);

    public static Error TipoCreditoObrigatorio =>
        new(
            code: "Credito.TipoCreditoObrigatorio",
            description: "O tipo do crédito é obrigatório.",
            type: ErrorType.Validation);

    public static Error AliquotaInvalida =>
        new(
            code: "Credito.AliquotaInvalida",
            description: "A alíquota informada deve ser maior ou igual a zero.",
            type: ErrorType.Validation);
}
