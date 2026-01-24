using BudgetApp.DataAccess.Interfaces;
using Dapper;
using System.Data;

namespace BudgetApp.DataAccess.Data;

public class BudgetAppDataAccess : IBudgetAppDataAccess
{
    private readonly IBudgetAppUnitOfWork _uow;

    public BudgetAppDataAccess(IBudgetAppUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<T>> QueryDataAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure
        )
    {
        return await _uow.Connection.QueryAsync<T>(
            queryString,
            parameters,
            transaction: _uow.Transaction,
            commandType: commandType);
    }

    public async Task ExecuteDataAsync<T>(
        string queryString,
        T parameters,
        CommandType commandType = CommandType.StoredProcedure)
    {
        await _uow.Connection.ExecuteAsync(
            queryString,
            parameters,
            transaction: _uow.Transaction,
            commandType: commandType);
    }
}
