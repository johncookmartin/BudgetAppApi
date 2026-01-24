using AuthLibrary.Data.Entities;
using AuthLibrary.DTOs.Google;
using AuthLibrary.DTOs.Login;
using AuthLibrary.DTOs.Register;
using BudgetApp.Application.Services.Auth.DTOs;

namespace BudgetApp.Application.Services.Auth.Interfaces;

public interface IAuthService
{
    Task<GenerateTokensResult> GenerateTokensAsync(AuthUser user, IList<string> roles); //
    Task<LoginResult> LoginAsync(LoginRequest request); //
    Task<GoogleLoginResult> LoginWithGoogleAsync(GoogleRequest request); //
    Task<AuthRefreshTokenResult> RefreshTokenAsync(string refreshToken); //
    Task<RegisterResult> RegisterAsync(RegisterRequest request); //
    Task<LogoutResult> LogoutAsync(string refreshToken); //
}