namespace BudgetApp.Domain.Entities;
public class BudgetUser
{
    public int Id { get; set; }
    public string GoogleSubject { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public BudgetUserRole Role { get; set; } = BudgetUserRole.Guest;

    private BudgetUser() { }
    public BudgetUser(string googleSubject, string email, string displayName, BudgetUserRole role)
    {
        GoogleSubject = googleSubject;
        Email = email;
        DisplayName = displayName;
        Role = role;
    }

    public void UpdateProfile(string email, string displayName)
    {
        Email = email;
        DisplayName = displayName;
    }
}
