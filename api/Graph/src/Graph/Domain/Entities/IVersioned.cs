namespace Graph.Domain.Entities;

public interface IVersioned
{
    long Version { get; set; }
}