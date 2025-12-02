using CreditoApi.SharedKernel.Errors;

namespace CreditoApi.Errors;

/// <summary>
/// Represents a standardized API error response containing a collection of errors.
/// </summary>
/// <param name="errors"></param>
public class ApiErrorResponse(IReadOnlyCollection<Error> errors)
{
    /// <summary>
    /// Gets the collection of errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; } = errors;
}
