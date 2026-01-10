using AuthLibrary.DTOs;
using AuthLibrary.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IGoogleService _googleService;

    public AuthController(IUserService userService,
        ITokenService tokenService,
        IGoogleService googleService)
    {
        _userService = userService;
        _tokenService = tokenService;
        _googleService = googleService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _userService.RegisterUser(request);
        if (!result.Succeeded)
        {
            return BadRequest(new { Errors = result.Errors });
        }

        return await LoginHandler(new LoginRequestDto
        {
            Email = request.Email,
            Password = request.Password,
        });
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        return await LoginHandler(request);
    }

    [HttpPost]
    [Route("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleRequestDto request)
    {
        var googleResult = await _googleService.LoginWithGoogleAsync(request);
        if (!googleResult.Succeeded)
        {
            return BadRequest(new { Errors = googleResult.Errors });
        }

        var user = await _userService.GetUserByEmailAsync(googleResult.Email);
        if (user is null)
        {
            var registerResult = await _userService.RegisterUser(new RegisterRequestDto
            {
                Email = googleResult.Email,
                Initials = _googleService.GenerateInitialsFromName(googleResult.FamilyName, googleResult.GivenName),
                Provider = "Google",
                ProviderKey = googleResult.Subject,
                Name = googleResult.DisplayName,
            });
            if (!registerResult.Succeeded)
            {
                return BadRequest(new { Errors = registerResult.Errors });
            }
            user = registerResult.User;
        }

        return await LoginHandler(new LoginRequestDto
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

        var validationResult = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (!validationResult.Succeeded)
        {
            return Unauthorized();
        }

        var user = validationResult.User;
        var accessToken = await _tokenService.GenerateTokenAsync(user, validationResult.Roles);
        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        SetRefreshTokenCookie(newRefreshToken);

        return Ok(new { AccessToken = accessToken, User = new { id = user.Id, email = user.Email } });

    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
        {
            return Unauthorized();
        }
        var existingToken = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (!existingToken.Succeeded)
        {
            return Unauthorized();
        }

        await _tokenService.RevokeRefreshTokensAsync(existingToken.User.Id);

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

    private async Task<IActionResult> LoginHandler(LoginRequestDto request)
    {
        var result = await _userService.LoginUser(request);
        if (!result.Succeeded)
        {
            return Unauthorized();
        }
        if (result.User is null || result.Roles is null)
        {
            return Problem(detail: "User or roles not found.", statusCode: StatusCodes.Status500InternalServerError);
        }

        var user = result.User;

        var accessToken = await _tokenService.GenerateTokenAsync(result.User, result.Roles);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(result.User);

        SetRefreshTokenCookie(refreshToken);
        return Ok(new { AccessToken = accessToken, User = new { id = user.Id, email = user.Email } });
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
