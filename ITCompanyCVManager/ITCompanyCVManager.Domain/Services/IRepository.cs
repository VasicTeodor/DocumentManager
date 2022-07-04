using System.Linq.Expressions;
using ITCompanyCVManager.Domain.Base;

namespace ITCompanyCVManager.Domain.Services;

public interface IRepository<TRoot>
    where TRoot : IAggregateRoot
{
    Task<TRoot> Find(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default);
    IQueryable<TRoot> GetQuery();
    Task<TRoot> FindWithoutInclude(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default);
    Task<List<TRoot>> FindCollection(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default);
    Task<List<TRoot>> FindCollectionWithoutInclude(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default);
    Task<bool> Exist(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default);
    Task Create(TRoot entity, CancellationToken token);
    void Delete(TRoot entity);
}