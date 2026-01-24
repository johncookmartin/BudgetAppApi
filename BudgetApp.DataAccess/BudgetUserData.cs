using BudgetApp.DataAccess.Interfaces;

namespace BudgetApp.DataAccess.Data;

public class BudgetUserData : IBudgetUserData
{
    private readonly IBudgetAppDataAccess _db;
    private readonly IBudgetAppUnitOfWork _uow;

    public BudgetUserData(IBudgetAppDataAccess db, IBudgetAppUnitOfWork uow)
    {
        _db = db;
        _uow = uow;
    }

    public async Task<int> CreateUserAsync(Guid authId)
    {
        int userId = 0;

        var parameters = new
        {
            AuthId = authId
        };

        try
        {
            userId = (await _db.QueryDataAsync<int, dynamic>(
            "stp_BudgetUser_Create",
            parameters,
            System.Data.CommandType.StoredProcedure)
            ).First();

            _uow.Commit();
        }
        catch (Exception ex)
        {
            _uow.Rollback();
            throw new Exception("Could not create user.", ex);
        }

        return userId;
    }
}

