using BudgetApp.Auth.Models;
using System.Security.Claims;

namespace BudgetApp.Auth.Extensions;
public static class ClaimsPrincipalExtension
{
    public static GoogleUser ToGoogleUser(this ClaimsPrincipal user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user));
        }

        string? sub = user.FindFirst("sub")?.Value;
        if (sub == null)
        {
            throw new InvalidOperationException("The 'sub' claim is missing.");
        }

        string? email = user.FindFirst("email")?.Value;
        if (email == null)
        {
            throw new InvalidOperationException("The 'email' claim is missing.");
        }

        string? name = user.FindFirst("name")?.Value;

        if (name == null)
        {
            throw new InvalidOperationException("The 'name' claim is missing.");
        }

        return new GoogleUser
        {
            Sub = sub,
            Email = email,
            Name = name,
            Picture = user.FindFirst("picture")?.Value,
            FamilyName = user.FindFirst("family_name")?.Value,
            GivenName = user.FindFirst("given_name")?.Value
        };
    }
}
