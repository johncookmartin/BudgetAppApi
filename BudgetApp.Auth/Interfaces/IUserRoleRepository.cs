using BudgetApp.Auth.Data.Entities;

namespace BudgetApp.Auth.Interfaces;
public interface IUserRoleRepository
{
    Task AddAsync(UserRole role);
    Task<List<UserRole>> GetAllAsync();
    Task<UserRole?> GetByIdAsync(int id);
    Task<UserRole?> GetByNameAsync(string roleName);
    Task<UserRole?> GetDefaultRoleAsync();
    Task SaveChangesAsync();
}