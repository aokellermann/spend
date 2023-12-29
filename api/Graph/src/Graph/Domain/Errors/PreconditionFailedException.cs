namespace Spend.Graph.Domain.Errors;

public class PreconditionFailedException : Exception
{
     public PreconditionFailedException(Type type, string message)
            : base($"Precondition on {type.Name} failed: {message}")
        {
            Data["Type"] = type.Name;
            Data["Message"] = message;
        }
}