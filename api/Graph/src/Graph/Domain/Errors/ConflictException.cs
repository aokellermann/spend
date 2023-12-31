namespace Spend.Graph.Domain.Errors;

public class ConflictException : Exception
{
    public ConflictException(Type type, object fieldValue)
        : base($"There already exists a {type.Name} with field value {fieldValue}")
    {
        Data["Type"] = type.Name;
        Data["FieldValue"] = fieldValue;
    }
}