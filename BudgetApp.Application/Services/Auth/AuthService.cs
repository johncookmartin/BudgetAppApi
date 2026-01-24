using AuthLibrary.Data.Entities;
using AuthLibrary.DTOs.Google;
using AuthLibrary.DTOs.Login;
using AuthLibrary.DTOs.Register;
using AuthLibrary.Services.Interfaces;
using BudgetApp.Application.Services.Auth.DTOs;
using BudgetApp.Application.Services.Auth.Interfaces;
using BudgetApp.DataAccess.Interfaces;

namespace BudgetApp.Application.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IGoogleService _googleService;
    private readonly IBudgetUserData _budgetUserData;

    public AuthService(IUserService userService, ITokenService tokenService, IGoogleService googleService, IBudgetUserData budgetUserData)
    {
        _userService = userService;
        _tokenService = tokenService;
        _googleService = googleService;
        _budgetUserData = budgetUserData;
    }


    public async Task<RegisterResult> RegisterAsync(RegisterRequest request)
    {
        var result = await _userService.RegisterUser(request);
        if (!result.Succeeded)
        {
            return result;
        }

        AuthUser user = result.User!;
        try
        {
            await _budgetUserData.CreateUserAsync(user.Id);
        }
        catch (Exception ex)
        {
            return RegisterResult.Failure(new[] { $"An error occurred while creating the user profile: {ex.Message}" });
        }

        return result;
    }
    public async Task<LoginResult> LoginAsync(LoginRequest request) => await _userService.LoginUser(request);
    public async Task<GenerateTokensResult> GenerateTokensAsync(AuthUser user, IList<string> roles)
    {
        var accessToken = await _tokenService.GenerateTokenAsync(user, roles);
        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(user);
        return GenerateTokensResult.Success(accessToken, refreshToken);
    }
    public async Task<AuthRefreshTokenResult> RefreshTokenAsync(string refreshToken)
    {
        var validationResult = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (!validationResult.Succeeded)
        {
            return AuthRefreshTokenResult.Failure(validationResult.ErrorMessage);
        }

        var user = validationResult.User;
        var accessToken = await _tokenService.GenerateTokenAsync(user, validationResult.Roles);
        var newRefreshToken = await _tokenService.GenerateRefreshTokenAsync(user);

        return AuthRefreshTokenResult.Success(accessToken, newRefreshToken, user);
    }
    public async Task<GoogleLoginResult> LoginWithGoogleAsync(GoogleRequest request)
    {
        var result = await _googleService.LoginWithGoogleAsync(request);
        if (!result.Succeeded)
        {
            return result;
        }

        var user = await _userService.GetUserByEmailAsync(result.Email);
        if (user == null)
        {
            var registerRequest = new RegisterRequest
            {
                Email = result.Email,
                Initials = _googleService.GenerateInitialsFromName(result.FamilyName, result.GivenName),
                Provider = "Google",
                ProviderKey = result.Subject,
                Name = result.DisplayName,
            };
            var registerResult = await RegisterAsync(registerRequest);

            if (!registerResult.Succeeded)
            {
                return GoogleLoginResult.Failure(registerResult.Errors!);
            }
            user = registerResult.User!;
        }

        return result;
    }
    public async Task<LogoutResult> LogoutAsync(string refreshToken)
    {
        var validationResult = await _tokenService.ValidateRefreshTokenAsync(refreshToken);
        if (!validationResult.Succeeded)
        {
            return LogoutResult.Failure(validationResult.ErrorMessage);
        }

        try
        {
            await _tokenService.RevokeRefreshTokensAsync(validationResult.Token.Id);
        }
        catch (Exception ex)
        {
            return LogoutResult.Failure($"An error occurred while revoking the refresh token: {ex.Message}");
        }

        return LogoutResult.Success();
    }
}
