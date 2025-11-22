using BudgetApp.Domain.Entities;

namespace BudgetApp.Application.Auth;
public interface IAuthService
{
    Task<BudgetUser> EnsureUserAsync(string googleSub, string email, string displayName, string? pictureUrl, string? familyName, string? givenName);
}