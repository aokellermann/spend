namespace Spend.Graph.Configuration;

public static class EnvironmentHelpers
{
    public static bool IsLambda { get; } = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("AWS_LAMBDA_FUNCTION_NAME"));
}