using BudgetApp.Auth.Data.Entities;
using BudgetApp.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Auth.Data.Repositories;
public class BudgetUserRepository : IBudgetUserRepository
{
    private readonly AuthDbContext _context;

    public BudgetUserRepository(AuthDbContext context)
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

    public void Update(BudgetUser user)
    {
        _context.BudgetUsers.Update(user);
    }
}