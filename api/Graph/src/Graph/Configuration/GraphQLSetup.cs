using Going.Plaid;
using HotChocolate.Subscriptions;
using Spend.Graph.Infrastructure;
using Spend.Graph.Types;

namespace Spend.Graph.Configuration;

public static class GraphQLSetup
{
    public static void AddGraph(this IServiceCollection services, IHostEnvironment environment)
    {
        services
            .AddGraphQLServer()
            .InitializeOnStartup()
            .AddQueryType()
            .AddMutationType()
            .AddMutationConventions(applyToAllMutations: true)
            // .AddDefaultTransactionScopeHandler()
            // .AddSubscriptionType()
            .AddProjections()
            .AddMongoDbProjections()
            .AddFiltering()
            .AddMongoDbFiltering()
            .AddSorting()
            .AddMongoDbSorting()
            .AddGraphTypes()
            .RegisterService<SpendDb>()
            .RegisterService<ITopicEventSender>()
            .RegisterService<PlaidClient>()
            .RegisterService<IHttpContextAccessor>()
            .RegisterService<UserContext>()
            .AddAuthorization()
            .TryAddTypeInterceptor<PlaidTypeInterceptor>()
            .AddObjectIdType()
            .ModifyRequestOptions(x => x.IncludeExceptionDetails = !environment.IsProduction())
            .AddDiagnosticEventListener<GraphQLErrorLogger>()
            ;
    }
}