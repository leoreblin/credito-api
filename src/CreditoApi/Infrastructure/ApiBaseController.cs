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
}
