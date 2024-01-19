using System.Text.Json;
using Microsoft.AspNetCore.Http.Json;
using Serilog;
using Spend.Graph.Configuration;

SerilogSetup.SetupBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SerilogSetup.ConfigureSerilog);

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

builder.Services.Configure<JsonOptions>(o => { o.SerializerOptions.AddPlaidConverters(); });

builder.Services.AddAuth(builder.Configuration);

builder.Services.AddGraph(builder.Environment);

builder.Services.AddHttpContextAccessor();

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddPlaid(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();
app.MapGraphQLHttp().RequireAuthorization();
app.MapGraphQLSchema();

await app.RunAsync();