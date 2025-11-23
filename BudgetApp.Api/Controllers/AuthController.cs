using BudgetApp.Api.Models;
using BudgetApp.Auth.Data.Entities;
using BudgetApp.Auth.Interfaces;
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
        BudgetUser budgetUser = await _authService.EnsureUserAsync(User);

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
