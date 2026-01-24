using AuthLibrary;
using BudgetApp.DataAccess.Data;
using BudgetApp.DataAccess.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices<TMigrationAssemblyMarker>(this IServiceCollection services, IConfiguration configuration)
    {
        // Add application services here
        services.AddAuthServices<TMigrationAssemblyMarker>(configuration);

        services.AddSingleton<IBudgetAppDataAccess, BudgetAppDataAccess>();
        services.AddSingleton<IBudgetUserData, BudgetUserData>();

        return services;
    }
}
