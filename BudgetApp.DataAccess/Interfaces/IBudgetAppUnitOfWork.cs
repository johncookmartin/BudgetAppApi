using System.Data;

namespace BudgetApp.DataAccess.Interfaces;

public interface IBudgetAppUnitOfWork
{
    IDbConnection Connection { get; }
    IDbTransaction Transaction { get; }

    void Commit();
    void Dispose();
    void Rollback();
}