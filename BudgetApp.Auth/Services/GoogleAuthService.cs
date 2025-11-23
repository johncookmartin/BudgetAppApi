using BudgetApp.Auth.Data.Entities;
using BudgetApp.Auth.Extensions;
using BudgetApp.Auth.Interfaces;
using BudgetApp.Auth.Models;
using System.Security.Claims;

namespace BudgetApp.Auth.Services;
public class GoogleAuthService : IAuthService
{
    private readonly IUserRoleRepository _roleRepo;
    private readonly IBudgetUserRepository _userRepo;

    public GoogleAuthService(IUserRoleRepository roleRepo, IBudgetUserRepository userRepo)
    {
        _roleRepo = roleRepo;
        _userRepo = userRepo;
    }

    public async Task<BudgetUser> EnsureUserAsync(ClaimsPrincipal principal)
    {
        GoogleUser googleUser = principal.ToGoogleUser();
        BudgetUser? budgetUser = await _userRepo.GetByGoogleSubjectAsync(googleUser.Sub);

        if (budgetUser is null)
        {
            UserRole? defaultRole = await _roleRepo.GetDefaultRoleAsync();
            if (defaultRole is null)
            {
                throw new InvalidOperationException("Default user role is not configured.");
            }

            budgetUser = new BudgetUser
            (
                googleSubject: googleUser.Sub,
                email: googleUser.Email,
                displayName: googleUser.Name,
                pictureUrl: googleUser.Picture,
                familyName: googleUser.FamilyName,
                givenName: googleUser.GivenName,
                role: defaultRole
            );
            await _userRepo.AddAsync(budgetUser);
        }
        else
        {
            budgetUser.Email = googleUser.Email;
            budgetUser.DisplayName = googleUser.Name;
            _userRepo.Update(budgetUser);
        }

        await _userRepo.SaveChangesAsync();
        return budgetUser;
    }
}
