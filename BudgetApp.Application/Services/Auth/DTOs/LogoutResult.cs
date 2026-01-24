namespace BudgetApp.Application.Services.Auth.DTOs;

public sealed record LogoutResult
{
    public bool Succeeded { get; init; }
    public string ErrorMessage { get; init; } = string.Empty;
    public static LogoutResult Success() =>
        new() { Succeeded = true };
    public static LogoutResult Failure(string errorMessage) =>
        new() { Succeeded = false, ErrorMessage = errorMessage };
}
