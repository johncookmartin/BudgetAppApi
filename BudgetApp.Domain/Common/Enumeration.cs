namespace BudgetApp.Domain.Common;
public class Enumeration : IComparable
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int CompareTo(object? other)
    {
        return Id.CompareTo(((Enumeration)other!).Id);
    }

    public override string ToString() => Name;
}
