using CreditoApi.SharedKernel.Errors;
using System.Diagnostics.CodeAnalysis;

namespace CreditoApi.SharedKernel.Results;

/// <summary>
/// Represents the result of an operation that may succeed or fail, with an optional value of type <typeparamref
/// name="TValue"/>.
/// </summary>
/// <remarks>This class extends <see cref="Result"/> to include a value when the operation succeeds. If
/// the operation fails, the value cannot be accessed, and an error is provided instead.</remarks>
/// <typeparam name="TValue">The type of the value associated with a successful result.</typeparam>
public class Result<TValue> : Result
{
    private readonly TValue? _value;

    public Result(TValue? value, bool isSuccess, Error error) 
        : base(isSuccess, error)
    {
        _value = value;
    }

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Cannot access the value of a failed result.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}
