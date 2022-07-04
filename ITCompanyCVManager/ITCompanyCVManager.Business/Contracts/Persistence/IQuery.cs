namespace ITCompanyCVManager.Business.Contracts.Persistence;

public interface IQuery<in TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request, CancellationToken token = default);
}

public interface IQuery<TResponse>
{
    Task<TResponse> Execute(CancellationToken token = default);
}