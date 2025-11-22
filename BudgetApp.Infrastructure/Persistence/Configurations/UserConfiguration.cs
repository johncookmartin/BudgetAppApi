using BudgetApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<BudgetUser>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BudgetUser> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.Property(u => u.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property<int>("Role").IsRequired();
        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey("Role")
               .IsRequired();
    }
}
