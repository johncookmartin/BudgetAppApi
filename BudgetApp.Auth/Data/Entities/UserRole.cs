namespace BudgetApp.Auth.Data.Entities;
public class UserRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    private UserRole() { }
    public UserRole(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
