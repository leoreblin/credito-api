using CreditoApi.SharedKernel.Errors;

namespace CreditoApi.SharedKernel.Results;

/// <summary>
/// Represents the outcome of an operation, encapsulating success or failure states along with an associated error,
/// if any.
/// </summary>
/// <remarks>The <see cref="Result"/> class is designed to provide a standardized way to represent the
/// result of an operation.  It distinguishes between success and failure states using the <see cref="IsSuccess"/>
/// property, and associates  an <see cref="Error"/> object with the result to provide additional context in case of
/// failure.  Use the static factory methods, such as <see cref="Success()"/> and <see cref="Failure(Error)"/>, to
/// create instances  of this class. For results that include a value, use the generic <see cref="Result{TValue}"/>
/// class.</remarks>
public class Result
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class with the specified success state and error.
    /// </summary>
    /// <param name="isSuccess">A value indicating whether the operation was successful.  <see langword="true"/> if the operation succeeded;
    /// otherwise, <see langword="false"/>.</param>
    /// <param name="error">The error associated with the operation. Must be <see cref="Error.None"/> if <paramref name="isSuccess"/> is
    /// <see langword="true"/>,  and must not be <see cref="Error.None"/> if <paramref name="isSuccess"/> is <see
    /// langword="false"/>.</param>
    /// <exception cref="ArgumentException">Thrown if the combination of <paramref name="isSuccess"/> and <paramref name="error"/> is invalid.</exception>
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation was unsuccessful.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error details associated with the current operation.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a successful <see cref="Result"/> instance with no associated error.
    /// </summary>
    /// <returns>A <see cref="Result"/> object representing a successful operation.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a successful result containing the specified value.
    /// </summary>
    /// <typeparam name="TValue">The type of the value contained in the result.</typeparam>
    /// <param name="value">The value to include in the successful result.</param>
    /// <returns>A <see cref="Result{TValue}"/> instance representing a successful operation with the specified value.</returns>
    public static Result<TValue> Success<TValue>(TValue value) =>
        new(value, true, Error.None);

    /// <summary>
    /// Creates a failed <see cref="Result"/> instance with the specified error.
    /// </summary>
    /// <param name="error">The error that describes the reason for the failure. Cannot be <see langword="null"/>.</param>
    /// <returns>A <see cref="Result"/> instance representing a failure, containing the specified error.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a failed result with the specified error.
    /// </summary>
    /// <typeparam name="TValue">The type of the value that the result would hold if it were successful.</typeparam>
    /// <param name="error">The error describing the reason for the failure. Cannot be null.</param>
    /// <returns>A result object representing a failure, containing the specified error.</returns>
    public static Result<TValue> Failure<TValue>(Error error) =>
        new(default, false, error);
}
