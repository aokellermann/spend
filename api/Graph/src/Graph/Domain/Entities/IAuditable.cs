namespace Spend.Graph.Domain.Entities;

/// <summary>
///     Auditable entity definitions.
/// </summary>
public interface IAuditable
{
    /// <summary>
    ///     UTC datetime when this entity was first created.
    /// </summary>
    public DateTime InsertedAt { get; }

    /// <summary>
    ///     UTC datetime when this entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; }
}