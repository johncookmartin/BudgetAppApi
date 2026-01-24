namespace BudgetApp.Application.Services.Auth.DTOs;

public sealed record GenerateTokensResult
{
    public bool Succeeded { get; init; }
    public string AccessToken { get; init; } = string.Empty;
    public string RefreshToken { get; init; } = string.Empty;
    public string ErrorMessage { get; init; } = string.Empty;
    public static GenerateTokensResult Success(string accessToken, string refreshToken) =>
        new() { Succeeded = true, AccessToken = accessToken, RefreshToken = refreshToken };
    public static GenerateTokensResult Failure(string errorMessage) =>
        new() { Succeeded = false, ErrorMessage = errorMessage };
}
