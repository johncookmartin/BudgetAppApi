using BudgetApp.Api.Models;
using BudgetApp.Application.Common;
using BudgetApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepo;
    private readonly IBudgetUserRoleRepository _roleRepo;

    public AuthController(IUserRepository userRepo, IBudgetUserRoleRepository roleRepo)
    {
        _userRepo = userRepo;
        _roleRepo = roleRepo;
    }

    [HttpPost("ensure-user")]
    [Authorize]
    public async Task<ActionResult<UserDto>> EnsureUser()
    {
        var (sub, email, name) = GetGoogleClaims();

        BudgetUser? user = await _userRepo.GetByGoogleSubjectAsync(sub);

        if (user is null)
        {
            BudgetUserRole guestRole = await _roleRepo.GetByNameAsync("GUEST");
            user = new BudgetUser(sub, email, name, guestRole);
            await _userRepo.AddAsync(user);
        }
        else
        {
            user.UpdateProfile(email, name);
        }

        await _userRepo.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Role = user.Role.Name
        };

        return Ok(userDto);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var (sub, _, _) = GetGoogleClaims();
        BudgetUser? user = await _userRepo.GetByGoogleSubjectAsync(sub);
        if (user is null)
        {
            return NotFound("User not found");
        }
        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            DisplayName = user.DisplayName,
            Role = user.Role.Name
        };
        return Ok(userDto);
    }

    private (string Sub, string Email, string Name) GetGoogleClaims()
    {
        var sub = User.FindFirst("sub")?.Value ?? string.Empty;
        if (string.IsNullOrEmpty(sub))
        {
            throw new Exception("Missing 'sub' claim");
        }

        var email = User.FindFirst("email")?.Value ?? string.Empty;
        var name = User.FindFirst("name")?.Value ?? string.Empty;

        return (sub, email, name);
    }
}
