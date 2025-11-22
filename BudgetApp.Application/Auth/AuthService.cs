using BudgetApp.Application.Common;
using BudgetApp.Domain.Entities;

namespace BudgetApp.Application.Auth;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepo;
    private readonly IBudgetUserRoleRepository _roleRepo;
    public AuthService(IUserRepository userRepo, IBudgetUserRoleRepository roleRepo)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
    }

    public async Task<BudgetUser> EnsureUserAsync(
        string googleSub,
        string email,
        string displayName,
        string? pictureUrl,
        string? familyName,
        string? givenName)
    {
        var user = await _userRepo.GetByGoogleSubjectAsync(googleSub);
        if (user is null)
        {
            BudgetUserRole defaultRole = await _roleRepo.GetDefaultRoleAsync();
            user = new BudgetUser
            (
                googleSubject: googleSub,
                email: email,
                displayName: displayName,
                pictureUrl: pictureUrl,
                familyName: familyName,
                givenName: givenName,
                role: defaultRole
            );
            await _userRepo.AddAsync(user);
        }
        else
        {
            user.UpdateProfile(email, displayName);
            _userRepo.Update(user);
        }

        await _userRepo.SaveChangesAsync();

        return user;
    }
}
