using ITCompanyCVManager.Domain.Base;
using ITCompanyCVManager.Domain.Services;

namespace ITCompanyCVManager.Business.Contracts.Persistence;

public interface IApplicationUnitOfWork :
    IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>()
        where TEntity : class, IAggregateRoot;
}