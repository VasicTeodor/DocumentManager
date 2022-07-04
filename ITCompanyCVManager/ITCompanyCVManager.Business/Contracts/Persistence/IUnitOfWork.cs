using System.Data;

namespace ITCompanyCVManager.Business.Contracts.Persistence;

public interface IUnitOfWork
{
    void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    void Rollback();
    Task Complete(CancellationToken token = default, bool commitTransaction = true);
}