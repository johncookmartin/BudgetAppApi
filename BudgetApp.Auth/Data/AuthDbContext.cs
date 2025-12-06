using BudgetApp.Auth.Data.Configurations;
using BudgetApp.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Auth.Data;
public class AuthDbContext : IdentityDbContext<BudgetUser>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<BudgetUser> BudgetUsers => Set<BudgetUser>();
    // public DbSet<UserRole> UserRoles => Set<UserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new BudgetUserConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
    }


}
