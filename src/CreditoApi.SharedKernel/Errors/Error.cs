namespace CreditoApi.SharedKernel.Errors;

public record Error
{
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
    public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.Failure);

    /// <summary>
    /// Initializes a new instance of the <see cref="Error"/> class with the specified error code, description, and
    /// type.
    /// </summary>
    /// <param name="code">The unique identifier for the error. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="description">A detailed description of the error. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="type">The category or type of the error, represented as an <see cref="ErrorType"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="code"/> is <see langword="null"/> or empty, or if <paramref name="description"/>
    /// is <see langword="null"/> or empty.</exception>
    public Error(string code, string description, ErrorType type)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code), "The error code cannot be null or empty.");
        Description = description ?? throw new ArgumentNullException(nameof(description), "The error description cannot be null or empty.");
        Type = type;
    }

    /// <summary>
    /// Gets the code associated with the current instance.
    /// </summary>
    public string Code { get; }

    /// <summary>
    /// Gets the description associated with the object.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Gets the type of the error represented by this instance.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Creates an <see cref="Error"/> instance representing a failure with the specified code and description.
    /// </summary>
    /// <param name="code">A string that uniquely identifies the error. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="description">A detailed description of the failure. Cannot be <see langword="null"/> or empty.</param>
    /// <returns>An <see cref="Error"/> object with the specified code, description, and an error type of <see
    /// cref="ErrorType.Failure"/>.</returns>
    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    /// <summary>
    /// Creates a validation error with the specified code and description.
    /// </summary>
    /// <param name="code">The unique code identifying the validation error. Cannot be null or empty.</param>
    /// <param name="description">A detailed description of the validation error. Cannot be null or empty.</param>
    /// <returns>An <see cref="Error"/> instance representing the validation error.</returns>
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    /// <summary>
    /// Creates a new <see cref="Error"/> instance representing a "Not Found" error.
    /// </summary>
    /// <param name="code">The error code that identifies the specific "Not Found" error.</param>
    /// <param name="description">A detailed description of the "Not Found" error.</param>
    /// <returns>An <see cref="Error"/> object with the specified code and description, and an error type of <see
    /// cref="ErrorType.NotFound"/>.</returns>
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    /// <summary>
    /// Creates a new <see cref="Error"/> instance representing a conflict error.
    /// </summary>
    /// <param name="code">The error code that identifies the specific conflict.</param>
    /// <param name="description">A description of the conflict error.</param>
    /// <returns>An <see cref="Error"/> object with the specified code, description, and an error type of <see
    /// cref="ErrorType.Conflict"/>.</returns>
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);

    /// <summary>
    /// Creates a new <see cref="Error"/> instance representing a problem with the specified code and description.
    /// </summary>
    /// <param name="code">A string that uniquely identifies the problem. Cannot be null or empty.</param>
    /// <param name="description">A detailed description of the problem. Cannot be null or empty.</param>
    /// <returns>An <see cref="Error"/> instance with the specified code, description, and an error type of <see
    /// cref="ErrorType.Problem"/>.</returns>
    public static Error Problem(string code, string description) =>
        new(code, description, ErrorType.Problem);
}
