namespace BudgetApp.Domain.Entities;
public class BudgetUserRole
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    private BudgetUserRole() { }
    public BudgetUserRole(int id, string name, string? description = null)
    {
        Id = id;
        Name = name;
        Description = description;
    }
}
