using AuthLibrary;
using BudgetApp.Application.Services.Auth;
using BudgetApp.Application.Services.Auth.Interfaces;
using BudgetApp.DataAccess.Data;
using BudgetApp.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddServices<TMigrationAssemblyMarker>(this IServiceCollection services, IConfiguration configuration)
    {
        // Auth
        services.AddAuthServices<TMigrationAssemblyMarker>(configuration);

        // Data
        services.AddScoped<IBudgetAppUnitOfWork, BudgetAppUnitOfWork>();
        services.AddScoped<IBudgetAppDataAccess, BudgetAppDataAccess>();
        services.AddScoped<IBudgetUserData, BudgetUserData>();

        // Services
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
