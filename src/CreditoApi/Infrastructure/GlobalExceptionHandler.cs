using CreditoApi.Errors;
using CreditoApi.SharedKernel.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace CreditoApi.Infrastructure;

/// <summary>
/// Represents a global exception handler that logs unhandled exceptions and returns standardized error responses.
/// </summary>
/// <param name="logger">The logger.</param>
internal sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unhandled exception occurred.");

        await HandleExceptionAsync(httpContext, exception, cancellationToken);

        return true;
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        (HttpStatusCode statusCode, IReadOnlyCollection<Error> errors) =
            GetErrorResponseDetails(exception);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        string response = JsonSerializer.Serialize(new ApiErrorResponse(errors), JsonSerializerOptions);

        await context.Response.WriteAsync(response, cancellationToken);
    }

    private static (HttpStatusCode statusCode, IReadOnlyCollection<Error> errors) GetErrorResponseDetails(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (
                HttpStatusCode.BadRequest, 
                validationException.Errors.Select(e =>
                {
                    return new Error(
                        code: e.ErrorCode,
                        description: e.ErrorMessage,
                        type: ErrorType.Validation);
                }).ToArray()),

            _ => (
                HttpStatusCode.InternalServerError, 
                new Error[] 
                { 
                    new("InternalServerError", "An unexpected error occurred.", ErrorType.Problem),
                    new("ExceptionMessage", exception.Message, ErrorType.Problem)
                })
        };
    }
}
