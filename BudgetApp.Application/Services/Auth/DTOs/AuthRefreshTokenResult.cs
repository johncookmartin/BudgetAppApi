using AuthLibrary.Data.Entities;

namespace BudgetApp.Application.Services.Auth.DTOs;

public sealed record AuthRefreshTokenResult
{
    public bool Succeeded { get; init; }
    public AuthUser? User { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;
    public static AuthRefreshTokenResult Success(string accessToken, string refreshToken, AuthUser user) =>
        new() { Succeeded = true, AccessToken = accessToken, RefreshToken = refreshToken, User = user };
    public static AuthRefreshTokenResult Failure(string errorMessage) =>
        new() { Succeeded = false, ErrorMessage = errorMessage };
}
