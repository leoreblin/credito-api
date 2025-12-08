using CreditoApi.Errors;
using CreditoApi.SharedKernel.Errors;
using Microsoft.AspNetCore.Mvc;

namespace CreditoApi.Infrastructure;

/// <summary>
/// Represents the base API controller.
/// </summary>
[Route("api")]
[ApiController]
public abstract class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>
    /// response based on the specified <see cref="Error"/>.
    /// </summary>
    /// <param name="errors">The array of errors.</param>
    /// <returns></returns>
    protected IActionResult BadRequest(Error[] errors) => BadRequest(new ApiErrorResponse(errors));

    /// <summary>
    /// Creates an <see cref="BadRequestObjectResult"/> that produces a <see cref="StatusCodes.Status400BadRequest"/>
    /// response based on the specified <see cref="Error"/>.
    /// </summary>
    /// <param name="error">The error.</param>
    /// <returns></returns>
    protected IActionResult BadRequest(Error error) => BadRequest(new ApiErrorResponse([error]));

    /// <summary>
    /// Converts an <see cref="Error"/> instance into an appropriate <see cref="IActionResult"/> based on the error
    /// type.
    /// </summary>
    /// <param name="error">The <see cref="Error"/> instance to process. Cannot be <see langword="null"/>.</param>
    /// <returns>An <see cref="IActionResult"/> representing the error: <list type="bullet"> <item><description><see
    /// cref="NotFoundResult"/> if the error type is <see cref="ErrorType.NotFound"/>.</description></item>
    /// <item><description><see cref="BadRequestResult"/> if the error type is <see cref="ErrorType.Validation"/> or
    /// <see cref="ErrorType.NullValue"/>.</description></item> <item><description><see cref="ConflictResult"/> if the
    /// error type is <see cref="ErrorType.Conflict"/>.</description></item> <item><description><see
    /// cref="BadRequestResult"/> for all other error types.</description></item> </list></returns>
    protected IActionResult FromError(Error error) =>
        error switch
        {
            null => BadRequest(new ApiErrorResponse([Error.NullValue])),
            _ when error.Type == ErrorType.NotFound => NotFound(new ApiErrorResponse([error])),
            _ when error.Type == ErrorType.Validation => BadRequest(new ApiErrorResponse([error])),
            _ when error.Type == ErrorType.Conflict => Conflict(new ApiErrorResponse([error])),
            _ => BadRequest(new ApiErrorResponse([error])),
        };
}
