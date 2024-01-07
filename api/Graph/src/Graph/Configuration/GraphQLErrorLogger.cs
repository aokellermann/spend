using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Execution.Processing;
using HotChocolate.Resolvers;

namespace Spend.Graph.Configuration;

public class GraphQLErrorLogger : ExecutionDiagnosticEventListener
{
    private readonly ILogger<GraphQLErrorLogger> _log;

    public GraphQLErrorLogger(ILogger<GraphQLErrorLogger> log)
    {
        _log = log;
    }

    public override void ResolverError(
        IMiddlewareContext context,
        IError error)
    {
        _log.LogError(error.Exception, error.Message);
    }

    public override void TaskError(
        IExecutionTask task,
        IError error)
    {
        _log.LogError(error.Exception, error.Message);
    }

    public override void RequestError(
        IRequestContext context,
        Exception exception)
    {
        _log.LogError(exception, "RequestError");
    }

    public override void SubscriptionEventError(
        SubscriptionEventContext context,
        Exception exception)
    {
        _log.LogError(exception, "SubscriptionEventError");
    }

    public override void SubscriptionTransportError(
        ISubscription subscription,
        Exception exception)
    {
        _log.LogError(exception, "SubscriptionTransportError");
    }
}