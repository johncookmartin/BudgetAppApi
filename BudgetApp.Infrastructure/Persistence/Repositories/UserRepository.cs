using BudgetApp.Application.Common;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Infrastructure.Persistence.Repositories;
public class UserRepository : IUserRepository
{
    private readonly BudgetAppDbContext _context;
    public UserRepository(BudgetAppDbContext context)
    {
        _context = context;
    }

    public Task AddAsync(BudgetUser user)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BudgetUser?> GetByGoogleSubjectAsync(string sub)
    {
        throw new NotImplementedException();
    }
}
