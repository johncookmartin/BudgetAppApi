using BudgetApp.Api.Identity;
using BudgetApp.Api.Models;
using BudgetApp.Application.Auth;
using BudgetApp.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("ensure-user")]
    [Authorize]
    public async Task<ActionResult<UserDto>> EnsureUser()
    {
        BudgetUser budgetUser = await _authService.EnsureUserAsync(
            googleSub: User.GetGoogleSub(),
            email: User.GetGoogleEmail(),
            displayName: User.GetGoogleName(),
            pictureUrl: User.GetGooglePicture(),
            familyName: User.GetGoogleFamilyName(),
            givenName: User.GetGoogleGivenName()
            );

        var userDto = new UserDto
        {
            Id = budgetUser.Id,
            Email = budgetUser.Email,
            DisplayName = budgetUser.DisplayName,
            Role = budgetUser.Role.Name
        };

        return Ok(userDto);
    }
}
