using BudgetApp.DataAccess.Data.Interfaces;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BudgetApp.DataAccess.Data;

public class BudgetAppDataAccess : IBudgetAppDataAccess
{
    private readonly IConfiguration _config;
    private const string CONNECTION_STRING_NAME = "Default";

    public BudgetAppDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> QueryDataAsync<T, U>(
        string queryString,
        U parameters,
        CommandType commandType = CommandType.StoredProcedure,
        string connectionStringName = CONNECTION_STRING_NAME)
    {
        string connectionString = _config.GetConnectionString(connectionStringName)!;

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(
            queryString,
            parameters,
            commandType: commandType);

        return rows;
    }

    public async Task ExecuteDataAsync<T>(
        string queryString,
        T parameters,
        CommandType commandType = CommandType.StoredProcedure,
        string connectionStringName = CONNECTION_STRING_NAME)
    {
        string connectionString = _config.GetConnectionString(connectionStringName)!;

        using IDbConnection connection = new SqlConnection(connectionString);

        var rows = await connection.QueryAsync<T>(
            queryString,
            parameters,
            commandType: commandType);
    }
}
