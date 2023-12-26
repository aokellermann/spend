using System.Reflection;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using Going.Plaid;
using Graph.Infrastructure;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

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
    .AddFiltering()
    .AddSorting()
    .AddGraphTypes()
    .RegisterDbContext<SpendDbContext>()
    .RegisterService<ITopicEventSender>()
    .RegisterService<PlaidClient>()
    .RegisterService<IHttpContextAccessor>()
    .RegisterService<UserContext>()
    .AddAuthorization()
    ;

builder.Services.AddHttpContextAccessor();

// validation
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// database
builder.Services.AddDbContext<SpendDbContext>(x =>
    x.UseNpgsql(builder.Configuration["Database:ConnectionString"] + ";Password=" +
                builder.Configuration["Database:Password"]));

builder.Services.AddPlaid(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQLHttp();
app.MapGraphQLSchema();

await app.RunAsync();