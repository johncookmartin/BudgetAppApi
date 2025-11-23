using BudgetApp.Auth.Data;
using BudgetApp.Auth.Data.Repositories;
using BudgetApp.Auth.Interfaces;
using BudgetApp.Auth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BudgetApp.Auth;
public static class DependencyInjection
{
    public static IServiceCollection AddAuthLayer(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(connectionString));

        string? googleClientId = configuration["Authentication:Google:ClientId"]
            ?? throw new InvalidOperationException("Missing Google ClientId");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://accounts.google.com";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "https://accounts.google.com",
                    ValidateAudience = true,
                    ValidAudience = googleClientId,
                    ValidateLifetime = true
                };

                options.MapInboundClaims = false;
            });

        services.AddScoped<IAuthService, GoogleAuthService>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IBudgetUserRepository, BudgetUserRepository>();
        return services;
    }
}
