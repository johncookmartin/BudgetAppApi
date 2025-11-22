using BudgetApp.Domain.Entities;
using BudgetApp.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Persistence;
public class BudgetAppDbContext : DbContext
{
    public BudgetAppDbContext(DbContextOptions<BudgetAppDbContext> options) : base(options)
    {
    }

    public DbSet<BudgetUser> Users => Set<BudgetUser>();
    public DbSet<BudgetUserRole> Roles => Set<BudgetUserRole>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
    }

}
