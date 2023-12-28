namespace Spend.Graph.Domain.Entities;

/// <summary>
///     Entity base class.
/// </summary>
/// <typeparam name="TId">Identifier type.</typeparam>
public abstract class AuditableEntityWithTenantBase<TId> :
    IEntity<TId>,
    IVersioned,
    IAuditable,
    IHasTenant
    where TId : notnull
{
    /// <inheritdoc />
    public TId Id { get; init; } = default!;

    /// <inheritdoc />
    public long Version { get; init; }

    /// <inheritdoc />
    public DateTime InsertedAt { get; init; }

    /// <inheritdoc />
    public DateTime UpdatedAt { get; init; }

    /// <inheritdoc />
    public Guid UserId { get; init; }
}