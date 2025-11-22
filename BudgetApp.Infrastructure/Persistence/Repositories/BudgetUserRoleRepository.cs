using BudgetApp.Application.Common;
using BudgetApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Persistence.Repositories;
public class BudgetUserRoleRepository : IBudgetUserRoleRepository
{
    private readonly BudgetAppDbContext _context;

    public BudgetUserRoleRepository(BudgetAppDbContext context)
    {
        _context = context;
    }

    public async Task<BudgetUserRole?> GetDefaultRoleAsync()
    {
        return await _context.BudgetUserRoles.FirstOrDefaultAsync(r => r.Name == "GUEST");
    }

    public async Task<BudgetUserRole?> GetByNameAsync(string roleName)
    {
        return await _context.BudgetUserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
    }

    public async Task<List<BudgetUserRole>> GetAllAsync()
    {
        return await _context.BudgetUserRoles.ToListAsync();
    }

    public async Task AddAsync(BudgetUserRole role)
    {
        await _context.BudgetUserRoles.AddAsync(role);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<BudgetUserRole?> GetByIdAsync(int id)
    {
        return await _context.BudgetUserRoles.FirstOrDefaultAsync(r => r.Id == id);
    }
}
