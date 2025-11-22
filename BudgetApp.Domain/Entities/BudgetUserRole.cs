using BudgetApp.Domain.Common;

namespace BudgetApp.Domain.Entities;
public class BudgetUserRole : Enumeration
{
    public static BudgetUserRole Guest = new(1, "GUEST");
    public static BudgetUserRole User = new(2, "USER");
    public static BudgetUserRole Admin = new(3, "ADMIN");
    private BudgetUserRole(int id, string name) : base(id, name)
    {
    }
    private BudgetUserRole() : base(1, "GUEST") { }
}
