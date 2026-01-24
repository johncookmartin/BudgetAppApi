using BudgetApp.DataAccess.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace BudgetApp.DataAccess.Data;

public class BudgetAppUnitOfWork : IBudgetAppUnitOfWork
{
    private const string CONNECTION_STRING_NAME = "Default";
    public IDbConnection Connection { get; }
    public IDbTransaction Transaction { get; }
    public BudgetAppUnitOfWork(IConfiguration config, string connectionString = CONNECTION_STRING_NAME)
    {
        Connection = new SqlConnection(config.GetConnectionString(connectionString)!);
        Connection.Open();
        Transaction = Connection.BeginTransaction();
    }

    public void Commit()
    {
        Transaction.Commit();
    }
    public void Rollback()
    {
        Transaction.Rollback();
    }

    public void Dispose()
    {
        Transaction?.Dispose();
        Connection?.Dispose();
    }
}
