namespace Spend.Graph.Domain.Errors;

public class NotFoundException : Exception
{
    public NotFoundException(Type type, object id)
        : base($"{type.Name} with ID {id} not found")
    {
        Data["Type"] = type.Name;
        Data["Id"] = id;
    }
}