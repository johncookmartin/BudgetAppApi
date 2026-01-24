using AuthLibrary.DTOs.Google;
using AuthLibrary.DTOs.Login;
using AuthLibrary.DTOs.Register;
using BudgetApp.Application.Services.Auth.DTOs;
using BudgetApp.Application.Services.Auth.Interfaces;
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

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return await LoginHandler(new LoginRequest
        {
            Email = request.Email,
            Password = request.Password,
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        return await LoginHandler(request);
    }

    [HttpPost]
    [Route("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleRequest request)
    {
        var googleResult = await _authService.LoginWithGoogleAsync(request);

        return await LoginHandler(new LoginRequest
        {
            Email = googleResult.Email,
            Provider = "Google",
            ProviderKey = googleResult.Subject,
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        AuthRefreshTokenResult result = await _authService.RefreshTokenAsync(refreshToken);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }

        var user = result.User!;
        SetRefreshTokenCookie(result.RefreshToken);

        return Ok(new { AccessToken = result.AccessToken, User = new { id = user.Id, email = user.Email } });

    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }

        var logoutResult = await _authService.LogoutAsync(refreshToken);
        if (!logoutResult.Succeeded)
        {
            return Unauthorized();
        }

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(-1),
            Path = "/api/auth"
        };
        Response.Cookies.Append("refreshToken", string.Empty, cookieOptions);

        return Ok();
    }

    private async Task<IActionResult> LoginHandler(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }
        if (result.User is null || result.Roles is null)
        {
            return Problem(detail: "User or roles not found.", statusCode: StatusCodes.Status500InternalServerError);
        }

        var user = result.User;
        var tokenResult = await _authService.GenerateTokensAsync(user, result.Roles);

        SetRefreshTokenCookie(tokenResult.RefreshToken);
        return Ok(new { AccessToken = tokenResult.AccessToken, User = new { id = user.Id, email = user.Email } });
    }

    private void SetRefreshTokenCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddDays(7),
            Path = "/api/auth"
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }
}
