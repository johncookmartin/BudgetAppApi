using System.Security.Claims;

namespace BudgetApp.Api.Identity;

public static class ClaimsPrincipalExtensions
{
    public static string GetGoogleSub(this ClaimsPrincipal user)
    {
        string? sub = user.FindFirst("sub")?.Value;
        if (string.IsNullOrEmpty(sub))
        {
            throw new InvalidOperationException("Missing google subject claim.");
        }

        return sub;
    }

    public static string GetGoogleEmail(this ClaimsPrincipal user)
    {
        return user.FindFirst("email")?.Value ?? string.Empty;
    }

    public static string GetGoogleName(this ClaimsPrincipal user)
    {
        return user.FindFirst("name")?.Value ?? string.Empty;
    }

    public static string? GetGooglePicture(this ClaimsPrincipal user)
    {
        return user.FindFirst("picture")?.Value;
    }

    public static string? GetGoogleFamilyName(this ClaimsPrincipal user)
    {
        return user.FindFirst("family_name")?.Value;
    }

    public static string? GetGoogleGivenName(this ClaimsPrincipal user)
    {
        return user.FindFirst("given_name")?.Value;
    }
}
