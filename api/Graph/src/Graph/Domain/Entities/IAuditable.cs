namespace Graph.Domain.Entities;

public interface IAuditable
{
    public DateTime InsertedAt { get; }

    public DateTime UpdatedAt { get; }
}