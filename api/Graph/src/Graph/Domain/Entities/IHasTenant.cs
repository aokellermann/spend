namespace Spend.Graph.Domain.Entities;

/// <summary>
///     Entities with tenancy.
/// </summary>
public interface IHasTenant
{
    /// <summary>
    ///     Globally unique identifier for the tenant.
    /// </summary>
    Guid UserId { get; }
}