using BudgetApp.Auth.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BudgetApp.Auth.Data.Configurations;
public class BudgetUserConfiguration : IEntityTypeConfiguration<BudgetUser>
{
    public void Configure(EntityTypeBuilder<BudgetUser> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.GoogleSubject).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.GoogleSubject).IsUnique();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(u => u.PictureUrl).HasMaxLength(512);
        builder.Property(u => u.FamilyName).HasMaxLength(100);
        builder.Property(u => u.GivenName).HasMaxLength(100);
        builder.Property(u => u.RoleId).IsRequired();
        builder.HasOne(u => u.Role)
               .WithMany()
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
