namespace BudgetApp.Auth.Models;
public class GoogleUser
{
    public string Sub { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Picture { get; set; }
    public string? FamilyName { get; set; }
    public string? GivenName { get; set; }
}
