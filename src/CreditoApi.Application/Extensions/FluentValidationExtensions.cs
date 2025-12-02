using CreditoApi.SharedKernel.Errors;
using FluentValidation;
using FluentValidation.Results;

namespace CreditoApi.Application.Extensions;

public static class FluentValidationExtensions
{
    /// <summary>
    /// Specifies a custom error to use if validation fails.
    /// </summary>
    /// <typeparam name="T">The type being validated.</typeparam>
    /// <typeparam name="TProperty">The property being validated.</typeparam>
    /// <param name="rule">The current rule.</param>
    /// <param name="error">The error to use.</param>
    /// <returns>The same rule builder.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="error"/> is <see langword="null"/>.</exception>
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule,
        Error error)
    {
        if (error is null)
        {
            throw new ArgumentNullException(nameof(error), "The error is required.");
        }

        return rule.WithErrorCode(error.Code).WithMessage(error.Description);
    }

    /// <summary>
    /// Maps the collection of <see cref="ValidationFailure"/> to an array of <see cref="Error"/>./>
    /// </summary>
    /// <param name="result"></param>
    /// <returns></returns>
    public static Error[] ToErrors(this ValidationResult result) =>
        [.. result.Errors.Select(error =>
        {
            return new Error(
                code: error.ErrorCode,
                description: error.ErrorMessage,
                type: ErrorType.Validation);
        })];
}
