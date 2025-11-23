using BudgetApp.Auth.Data.Entities;
using BudgetApp.Auth.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Auth.Data.Repositories;
public class UserRoleRepository : IUserRoleRepository
{
    private readonly AuthDbContext _context;

    public UserRoleRepository(AuthDbContext context)
    {
        _context = context;
    }

    public async Task<UserRole?> GetDefaultRoleAsync()
    {
        return await _context.UserRoles.FirstOrDefaultAsync(r => r.Name == "GUEST");
    }

    public async Task<UserRole?> GetByNameAsync(string roleName)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(r => r.Name == roleName);
    }

    public async Task<List<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task AddAsync(UserRole role)
    {
        await _context.UserRoles.AddAsync(role);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<UserRole?> GetByIdAsync(int id)
    {
        return await _context.UserRoles.FirstOrDefaultAsync(r => r.Id == id);
    }
}
