
using BudgetApp.Domain.Entities;

namespace BudgetApp.Application.Common;
public interface IUserRepository
{
    Task AddAsync(BudgetUser user);
    Task<BudgetUser?> GetByGoogleSubjectAsync(string sub);
    Task SaveChangesAsync();
    void Update(BudgetUser user);
}
