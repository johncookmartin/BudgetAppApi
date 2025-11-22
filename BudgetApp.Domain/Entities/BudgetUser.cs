namespace BudgetApp.Domain.Entities;
public class BudgetUser
{
    public int Id { get; set; }
    public string GoogleSubject { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public string? FamilyName { get; set; }
    public string? GivenName { get; set; }
    public int RoleId { get; set; }
    public BudgetUserRole Role { get; set; } = null!;

    private BudgetUser() { }
    public BudgetUser(
        string googleSubject,
        string email,
        string displayName,
        string? pictureUrl,
        string? familyName,
        string? givenName,
        BudgetUserRole role)
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

    public void UpdateProfile(string email, string displayName)
    {
        Email = email;
        DisplayName = displayName;
    }

    public void ChangeRole(BudgetUserRole newRole)
    {
        RoleId = newRole.Id;
        Role = newRole;
    }
}
