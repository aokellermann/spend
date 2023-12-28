namespace Spend.Graph.Domain.Entities;

/// <summary>
///     Entity definition.
/// </summary>
/// <typeparam name="TId">The identifier type.</typeparam>
public interface IEntity<out TId>
    where TId : notnull
{
    /// <summary>
    ///     Entity identifier.
    /// </summary>
    TId Id { get; }
}