using BudgetApp.DataAccess.Interfaces;
using BudgetApp.DataAccess.Models;

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

    public async Task<BudgetUserModel> GetBudgetUser(Guid authId)
    {
        int userId = 0;

        try
        {
            var parameters = new
            {
                AuthId = authId
            };
            userId = (await _db.QueryDataAsync<int, dynamic>(
                "stp_BudgetUserId_GetByAuthId",
                parameters,
                System.Data.CommandType.StoredProcedure
            )).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve user.", ex);
        }
        if (userId == 0)
        {
            throw new Exception("User not found.");
        }

        return await GetBudgetUser(userId);

    }

    public async Task<BudgetUserModel> GetBudgetUser(int userId)
    {
        BudgetUserModel? userModel = null;
        try
        {
            var parameters = new
            {
                Id = userId
            };
            userModel = (await _db.QueryDataAsync<BudgetUserModel, dynamic>(
                "stp_BudgetUser_GetById",
                parameters,
                System.Data.CommandType.StoredProcedure
            )).FirstOrDefault();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not retrieve user.", ex);
        }
        if (userModel == null)
        {
            throw new Exception("User not found.");
        }
        return userModel;
    }

    public async Task UpdateBudgetUser(BudgetUserModel userModel)
    {
        var parameters = new
        {
            Id = userModel.Id,
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            ProfilePictureUrl = userModel.ProfilePictureUrl,
        };
        try
        {
            await _db.ExecuteDataAsync(
                "stp_BudgetUser_Update",
                parameters,
                System.Data.CommandType.StoredProcedure
            );
            _uow.Commit();
        }
        catch (Exception ex)
        {
            _uow.Rollback();
            throw new Exception("Could not update user.", ex);
        }
    }
}
