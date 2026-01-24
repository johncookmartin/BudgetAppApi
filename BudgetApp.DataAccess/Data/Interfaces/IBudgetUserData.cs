namespace BudgetApp.DataAccess.Data.Interfaces;

public interface IBudgetUserData
{
    Task<int> CreateUserAsync(Guid authId);
}