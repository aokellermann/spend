namespace Spend.Graph.Domain.Entities;

/// <summary>
///     Definition for versioned entities.
/// </summary>
public interface IVersioned
{
    /// <summary>
    ///     Entity version. Starts at 0 for new entities.
    /// </summary>
    long Version { get; }
}