using BudgetApp.Application.Common;
using BudgetApp.Infrastructure.Persistence;
using BudgetApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetApp.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BudgetAppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
