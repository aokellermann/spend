using Microsoft.AspNetCore.Authentication.JwtBearer;
using Spend.Graph.Infrastructure;

namespace Spend.Graph.Configuration;

public static class AuthSetup
{
    public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.Authority = configuration["Auth:Authority"];
                x.Audience = configuration["Auth:Audience"];
                x.TokenValidationParameters.ValidateAudience = false; // cognito quirk
            });

        services.AddAuthorization();

        services.AddScoped<UserContext>();
    }
}