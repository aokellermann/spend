namespace Graph.Domain.Entities;

public interface IHasTenant
{
    Guid UserId { get; }
}