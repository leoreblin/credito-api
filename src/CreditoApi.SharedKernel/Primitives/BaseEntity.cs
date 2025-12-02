namespace CreditoApi.SharedKernel.Primitives;

/// <summary>
/// Represents the base class for all entities, providing a unique identifier for each instance.
/// </summary>
/// <remarks>This class is intended to be inherited by other entity classes to ensure a consistent
/// implementation of a unique identifier. The <see cref="Id"/> property is initialized with a new GUID by default,
/// but can also be set explicitly through the constructor.</remarks>
public abstract class BaseEntity
{
    /// <summary>
    /// Gets the unique identifier for the instance.
    /// </summary>
    public long Id { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class with the specified unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier for the entity.</param>
    protected BaseEntity(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseEntity"/> class.
    /// </summary>
    /// <remarks>This constructor is protected and intended to be used by derived classes to
    /// initialize the base state of an entity. It ensures that the base class is properly constructed before any
    /// additional initialization in derived classes.</remarks>
    protected BaseEntity() { }
}
