using System.Reflection;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using Going.Plaid;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Spend.Graph.Infrastructure;
using Spend.Graph.Types;

var builder = WebApplication.CreateBuilder(args);

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.Configure<JsonOptions>(o => { o.SerializerOptions.AddPlaidConverters(); });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.Authority = builder.Configuration["Auth:Authority"];
        x.Audience = builder.Configuration["Auth:Audience"];
        x.TokenValidationParameters.ValidateAudience = false; // cognito quirk
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<UserContext>();

builder.Services
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

builder.Services.AddHttpContextAccessor();

// validation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// database
var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("camelCase", conventionPack, _ => true);
#pragma warning disable CS0618
BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;
#pragma warning restore CS0618
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
builder.Services.AddScoped<IMongoDatabase>(
    _ =>
    {
        var connectionString = builder.Configuration["Mongo:ConnectionString"]!
            .Replace("<password>", builder.Configuration["Mongo:Password"]!);
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        var loggerFactory = LoggerFactory.Create(b =>
        {
            b.AddSimpleConsole();
        });
        settings.LoggingSettings = new LoggingSettings(loggerFactory);
        return new MongoClient(settings).GetDatabase("Graph");
    });
builder.Services.AddScoped<SpendDb>();

builder.Services.AddPlaid(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQLHttp();
app.MapGraphQLSchema();

await app.RunAsync();