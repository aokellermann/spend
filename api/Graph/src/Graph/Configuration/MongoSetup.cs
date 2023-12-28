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

        var connectionString = configuration["Mongo:ConnectionString"]!
            .Replace("<password>", configuration["Mongo:Password"]!);
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        var loggerFactory = LoggerFactory.Create(b => { b.AddSimpleConsole(); });
        settings.LoggingSettings = new LoggingSettings(loggerFactory);

        var client = new MongoClient(settings).GetDatabase("Graph");

        services.AddScoped<IMongoDatabase>(_ => client);
        services.AddScoped<SpendDb>();
    }
}