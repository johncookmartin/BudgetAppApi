using Microsoft.AspNetCore.Identity;

namespace BudgetApp.Auth.Data.Entities;
public class BudgetUser : IdentityUser
{
    public string GoogleSubject { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public string? FamilyName { get; set; }
    public string? GivenName { get; set; }
    public int RoleId { get; set; }
    public UserRole Role { get; set; } = null!;

    private BudgetUser() { }
    public BudgetUser(
        string googleSubject,
        string email,
        string displayName,
        string? pictureUrl,
        string? familyName,
        string? givenName,
        UserRole role)
    {
        GoogleSubject = googleSubject;
        Email = email;
        DisplayName = displayName;
        PictureUrl = pictureUrl;
        FamilyName = familyName;
        GivenName = givenName;
        RoleId = role.Id;
        Role = role;
    }
}
