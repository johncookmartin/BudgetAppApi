using BudgetApp.Auth.Data.Entities;
using System.Security.Claims;

namespace BudgetApp.Auth.Interfaces;
public interface IAuthService
{
    Task<BudgetUser> EnsureUserAsync(ClaimsPrincipal principal);
}