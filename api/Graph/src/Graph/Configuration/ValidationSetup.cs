using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Spend.Graph.Configuration;

public static class ValidationSetup
{
    public static void AddValidation(this IServiceCollection services)
    {
        services
            .AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}