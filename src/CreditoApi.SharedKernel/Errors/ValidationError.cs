using CreditoApi.SharedKernel.Results;

namespace CreditoApi.SharedKernel.Errors;

public sealed record ValidationError : Error
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class with the specified array of errors.
    /// </summary>
    /// <param name="errors">The array of errors.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="errors"/> is
    /// <see langword="null"/>.</exception>
    public ValidationError(Error[] errors)
        : base(
              "Validation.General",
              "One or more validation errors occurred.",
              ErrorType.Validation)
    {
        Errors = errors ?? throw new ArgumentNullException(nameof(errors), "The errors array cannot be null.");
    }

    /// <summary>
    /// Gets the array of validation errors.
    /// </summary>
    public Error[] Errors { get; }

    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new([.. results.Where(r => r.IsFailure).Select(r => r.Error)]);
}
