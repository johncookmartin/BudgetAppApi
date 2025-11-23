using BudgetApp.Auth.Data.Entities;

namespace BudgetApp.Auth.Interfaces;
public interface IBudgetUserRepository
{
    Task AddAsync(BudgetUser user);
    Task<BudgetUser?> GetByGoogleSubjectAsync(string sub);
    Task SaveChangesAsync();
    void Update(BudgetUser user);
}