using System.Linq.Expressions;
using ITCompanyCVManager.Domain.Base;
using ITCompanyCVManager.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ITCompanyCVManager.Persistence.Repositories;

public abstract class BaseRepository<TRoot>
    where TRoot : class, IAggregateRoot
{
    protected DbSet<TRoot> _entityContext;
    protected readonly ApplicationDbContext _context;

    protected BaseRepository(ApplicationDbContext context)
    {
        _entityContext = context.Set<TRoot>();
        _context = context;
    }

    public virtual Task<TRoot> Find(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default)
    {
        return FindWithoutInclude(predicate, token);
    }

    public virtual Task<TRoot> FindWithoutInclude(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default)
    {
        return _entityContext.SingleOrDefaultAsync(predicate, token);
    }

    public virtual IQueryable<TRoot> GetQuery()
    {
        return _entityContext.AsQueryable();
    }

    public virtual Task<bool> Exist(Expression<Func<TRoot, bool>> predicate, CancellationToken token = default)
    {
        return _entityContext.AnyAsync(predicate, token);
    }

    public virtual Task Create(TRoot entity, CancellationToken token)
    {
        return _entityContext.AddAsync(entity, token).AsTask();
    }

    public virtual void Delete(TRoot entity)
    {
        _entityContext.Remove(entity);
    }

    public Task<TRoot> FindBySpecification(Expression<Func<TRoot, bool>> predicate,
        Func<IQueryable<TRoot>, IQueryable<TRoot>> query,
        CancellationToken token = default)
    {
        var queryBase = GetQuery();
        var where = queryBase.Where(predicate);

        var queryResult = query(where);

        return queryResult.SingleOrDefaultAsync(token);

    }
}