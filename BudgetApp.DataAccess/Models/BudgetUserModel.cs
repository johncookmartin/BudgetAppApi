namespace BudgetApp.DataAccess.Models;

public class BudgetUserModel
{
    public int Id { get; set; }
    public Guid AuthId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

}
