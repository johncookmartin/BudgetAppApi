using BudgetApp.Domain.Entities;
using BudgetApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Persistence;
public class BudgetAppDbContext : DbContext
{
    public BudgetAppDbContext(DbContextOptions<BudgetAppDbContext> options) : base(options)
    {
    }

    public DbSet<BudgetUser> BudgetUsers => Set<BudgetUser>();
    public DbSet<BudgetUserRole> BudgetUserRoles => Set<BudgetUserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new BudgetUserConfiguration());
        modelBuilder.ApplyConfiguration(new BudgetUserRoleConfiguration());
    }

}
