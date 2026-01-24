namespace BudgetApp.DataAccess.Interfaces;

public interface IBudgetUserData
{
    Task<int> CreateUserAsync(Guid authId);
}