using BudgetApp.DataAccess.Data.Interfaces;

namespace BudgetApp.DataAccess.Data;

public class BudgetUserData : IBudgetUserData
{
    private readonly IBudgetAppDataAccess _db;

    public BudgetUserData(IBudgetAppDataAccess db)
    {
        _db = db;
    }

    public async Task<int> CreateUserAsync(string authId)
    {
        var parameters = new
        {
            AuthId = authId
        };
        int userId = (await _db.QueryDataAsync<int, dynamic>(
            "stp_BudgetUser_Create",
            parameters,
            System.Data.CommandType.StoredProcedure)
            ).First();
        return userId;
    }
}
