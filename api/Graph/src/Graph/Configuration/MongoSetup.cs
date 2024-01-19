using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.Configuration;

public static class MongoSetup
{
    public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, _ => true);

#pragma warning disable CS0618
        BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
        BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618
        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        var connectionString =
            // if running on VPC, need to use PrivateLink endpoint
            MongoUrl.Create(configuration[$"Mongo:{(EnvironmentHelpers.IsLambda ? "Vpc" : "Internet")}ConnectionString"]!
            .Replace("<password>", configuration["Mongo:Password"]!));
        var settingsServerApi = new ServerApi(ServerApiVersion.V1);
        services.AddScoped<IMongoDatabase>(sp =>
        {
            var settings = MongoClientSettings.FromUrl(connectionString);
            settings.ServerApi = settingsServerApi;
            settings.LoggingSettings = new LoggingSettings(sp.GetRequiredService<ILoggerFactory>());

            return new MongoClient(settings).GetDatabase("Graph");
        });
        services.AddScoped<SpendDb>();
    }
}