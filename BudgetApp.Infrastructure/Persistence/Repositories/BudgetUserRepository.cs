using BudgetApp.Application.Common;
using BudgetApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Persistence.Repositories;
public class BudgetUserRepository : IUserRepository
{
    private readonly BudgetAppDbContext _context;
    public BudgetUserRepository(BudgetAppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(BudgetUser user)
    {
        await _context.BudgetUsers.AddAsync(user);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<BudgetUser?> GetByGoogleSubjectAsync(string sub)
    {
        return await _context.BudgetUsers.Include(u => u.Role).FirstOrDefaultAsync(u => u.GoogleSubject == sub);
    }
}
