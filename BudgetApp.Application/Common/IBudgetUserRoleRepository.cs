using BudgetApp.Domain.Entities;

namespace BudgetApp.Application.Common;
public interface IBudgetUserRoleRepository
{
    Task AddAsync(BudgetUserRole role);
    Task<List<BudgetUserRole>> GetAllAsync();
    Task<BudgetUserRole?> GetByIdAsync(int id);
    Task<BudgetUserRole?> GetByNameAsync(string roleName);
    Task SaveChangesAsync();
}
