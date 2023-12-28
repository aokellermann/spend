using Going.Plaid;
using HotChocolate.Subscriptions;
using MongoDB.Bson;
using Spend.Graph.Infrastructure;
using Spend.Graph.Types;

namespace Spend.Graph.Configuration;

public static class GraphQLSetup
{
    public static void AddGraph(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .InitializeOnStartup()
            .AddQueryType()
            .AddMutationType()
            .AddMutationConventions()
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
            .BindRuntimeType<ObjectId, IdType>()
            .AddTypeConverter<ObjectId, string>(o => o.ToString())
            .AddTypeConverter<string, ObjectId>(ObjectId.Parse)
            ;
    }
}